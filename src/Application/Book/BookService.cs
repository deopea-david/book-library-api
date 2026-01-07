using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Application.Common.Services;
using BookLibraryAPI.Domain.Entities;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookLibraryAPI.Application.Book;

public class BookService(IAppDbContext context, ILogger<BookService> logger)
  : DbService<BookItem>(context, context.Books, logger),
    IBookService
{
  public async Task<IEnumerable<BookItem>> GetMany(
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
      .Cacheable()
      .ToListAsync();
  }
}
