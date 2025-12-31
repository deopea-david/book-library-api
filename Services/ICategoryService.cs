using BookLibraryAPI.Models;

namespace BookLibraryAPI.Services;

public interface ICategoryService : IService<Category>
{
  public Task<IEnumerable<Category>> GetMany(int? id = null, string? name = null);
}
