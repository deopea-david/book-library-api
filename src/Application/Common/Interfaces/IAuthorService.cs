using BookLibraryAPI.Domain.Entities;

namespace BookLibraryAPI.Application.Common.Interfaces;

public interface IAuthorService : IService<AuthorItem>
{
  public Task<IEnumerable<AuthorItem>> GetMany(
    int? id = null,
    string? name = null,
    int? page = DefaultPage,
    int? size = DefaultPageSize
  );
}
