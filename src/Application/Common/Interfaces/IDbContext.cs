using BookLibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Application.Common.Interfaces;

public interface IAppDbContext
{
  DbSet<AuthorItem> Authors { get; set; }
  DbSet<BookItem> Books { get; set; }
  DbSet<CategoryItem> Categories { get; set; }
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
