using BookLibraryAPI.Data;
using BookLibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Services;

public class BookService(SQLiteContext context, ILogger<BookService> logger)
  : DbService<Book>(context, context.Books, logger),
    IBookService
{
  public async Task<IEnumerable<Book>> GetMany(
    int? authorId,
    int? categoryId,
    string? title,
    DateTime? publishedAt,
    string? isbn,
    bool? isRead
  )
  {
    return await context
      .Books.Where(
        (book) =>
          (authorId == null || authorId == book.AuthorId)
          && (categoryId == null || categoryId == book.CategoryId)
          && (title == null || title == book.Title)
          && (publishedAt == null || ((DateTime)publishedAt).Date == book.PublishedAt.Date)
          && (isbn == null || isbn == book.ISBN)
          && (isRead == null || isRead == book.IsRead)
      )
      .ToListAsync();
  }
}
