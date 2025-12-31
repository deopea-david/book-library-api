using BookLibraryAPI.Models;

namespace BookLibraryAPI.Services;

public interface IBookService : IService<Book>
{
  public Task<IEnumerable<Book>> GetMany(
    int? authorId = null,
    int? categoryId = null,
    string? title = null,
    DateTime? publishedAt = null,
    string? isbn = null,
    bool? isRead = null
  );
}
