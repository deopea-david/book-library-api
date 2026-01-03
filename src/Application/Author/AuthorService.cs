using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Application.Common.Services;
using BookLibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookLibraryAPI.Application.Author;

public class AuthorService(IAppDbContext context, ILogger<AuthorService> logger)
  : DbService<AuthorItem>(context, context.Authors, logger),
    IAuthorService
{
  public async Task<IEnumerable<AuthorItem>> GetMany(int? id = null, string? name = null)
  {
    return await context
      .Authors.Where((a) => (id == null || id == a.Id) && (name == null || name == a.Name))
      .ToListAsync();
  }
}
