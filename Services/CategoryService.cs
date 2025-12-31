using BookLibraryAPI.Data;
using BookLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Services;

public class CategoryService(SQLiteContext context, ILogger<CategoryService> logger)
  : DbService<Category>(context, context.Categories, logger),
    ICategoryService
{
  public async Task<IEnumerable<Category>> GetMany(int? id, string? name)
  {
    return await context
      .Categories.Where((c) => (id == null || id == c.Id) && (name == null || name == c.Name))
      .ToListAsync();
  }
}
