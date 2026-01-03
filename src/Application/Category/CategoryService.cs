using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Application.Common.Services;
using BookLibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Application.Category;

public class CategoryService(IDbContext context, ILogger<CategoryService> logger)
  : DbService<CategoryItem>(context, context.Categories, logger),
    ICategoryService
{
  public async Task<IEnumerable<CategoryItem>> GetMany(int? id, string? name)
  {
    return await context
      .Categories.Where((c) => (id == null || id == c.Id) && (name == null || name == c.Name))
      .ToListAsync();
  }
}
