using BookLibraryAPI.Data;
using BookLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Services;

public class AuthorService(SQLiteContext context, ILogger<AuthorService> logger)
  : DbService<Author>(context, context.Authors, logger),
    IAuthorService
{
  public async Task<IEnumerable<Author>> GetMany(int? id = null, string? name = null)
  {
    return await context
      .Authors.Where((a) => (id == null || id == a.Id) && (name == null || name == a.Name))
      .ToListAsync();
  }
}
