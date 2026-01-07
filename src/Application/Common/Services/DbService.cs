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

  public async Task<IEnumerable<T>> GetMany()
  {
    return await table.Cacheable().ToListAsync();
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
