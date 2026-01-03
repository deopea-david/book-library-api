using BookLibraryAPI.Domain.Entities;

namespace BookLibraryAPI.Application.Common.Interfaces;

public interface IBookService : IService<BookItem>
{
  public Task<IEnumerable<BookItem>> GetMany(
    int? authorId = null,
    int? categoryId = null,
    string? title = null,
    DateTime? publishedAt = null,
    string? isbn = null,
    bool? isRead = null
  );
}
