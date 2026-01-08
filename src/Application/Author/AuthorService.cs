using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Application.Common.Services;
using BookLibraryAPI.Domain.Entities;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookLibraryAPI.Application.Author;

public class AuthorService(IAppDbContext context, ILogger<AuthorService> logger)
  : DbService<AuthorItem>(context, context.Authors, logger),
    IAuthorService
{
  public async Task<IEnumerable<AuthorItem>> GetMany(int? id, string? name, int? page, int? size)
  {
    var (safePage, safeSize) = ParsePageAndSize(page, size);
    return await context
      .Authors.Where((a) => (id == null || id == a.Id) && (name == null || name == a.Name))
      .Skip((safePage - 1) * safeSize)
      .Take(safeSize)
      .Cacheable()
      .ToListAsync();
  }
}
