using BookLibraryAPI.Domain.Entities;

namespace BookLibraryAPI.Application.Common.Interfaces;

public interface ICategoryService : IService<CategoryItem>
{
  public Task<IEnumerable<CategoryItem>> GetMany(int? id = null, string? name = null);
}
