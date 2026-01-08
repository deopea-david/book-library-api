using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Domain.Common;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookLibraryAPI.Application.Common.Services;

public abstract partial class DbService<T>(IAppDbContext context, DbSet<T> table, ILogger<DbService<T>> logger)
  : IService<T>
  where T : class, IEntity
{
  public const int DefaultPage = 1;
  public const int DefaultPageSize = 25;

  public (int page, int size) ParsePageAndSize(int? page, int? size)
  {
    var safePage = page == null ? DefaultPage : int.Max((int)page, 1);
    var saveSize = size == null ? DefaultPageSize : int.Max((int)size, 1);
    return (safePage, saveSize);
  }

  public async Task<int> Delete(T entity)
  {
    table.Remove(entity);
    await context.SaveChangesAsync();
    return entity.Id;
  }

  public async Task<T?> GetById(int id)
  {
    return await table.Cacheable().FirstAsync(e => e.Id == id);
  }

  public async Task<IEnumerable<T>> GetMany(int? page = null, int? size = null)
  {
    var (safePage, safeSize) = ParsePageAndSize(page, size);
    return await table.Cacheable().Skip((safePage - 1) * safeSize).Take(safeSize).ToListAsync();
  }

  public virtual T OnCreate(T entity)
  {
    entity.CreatedAt = DateTime.UtcNow;
    return entity;
  }

  public async Task<T> Create(T entity)
  {
    OnCreate(entity);
    await table.AddAsync(entity);
    await context.SaveChangesAsync();
    return entity;
  }

  public virtual T OnUpdate(T entity)
  {
    entity.UpdatedAt = DateTime.UtcNow;
    return entity;
  }

  public async Task<T> Update<U>(U updates, T original)
    where U : class
  {
    foreach (var prop in typeof(U).GetProperties())
    {
      var val = prop.GetValue(updates);
      if (val != null)
        prop.SetValue(original, val);
    }
    OnUpdate(original);
    await context.SaveChangesAsync();

    return original;
  }
}
