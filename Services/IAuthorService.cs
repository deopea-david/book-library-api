using BookLibraryAPI.Models;

namespace BookLibraryAPI.Services;

public interface IAuthorService : IService<Author>
{
  public Task<IEnumerable<Author>> GetMany(int? id = null, string? name = null);
}
