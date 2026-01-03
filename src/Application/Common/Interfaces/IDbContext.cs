using BookLibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Application.Common.Interfaces;

public interface IDbContext
{
  DbSet<AuthorItem> Authors { get; set; }
  DbSet<BookItem> Books { get; set; }
  DbSet<CategoryItem> Categories { get; set; }
  Task<int> SaveChangesAsync();
  Task<int> SaveChangesAsync(CancellationToken? cancellationToken);
}
