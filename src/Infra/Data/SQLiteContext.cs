using BookLibraryAPI.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Infra.Data;

public class SQLiteContext(DbContextOptions<SQLiteContext> options) : DbContext(options)
{
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite(new SqliteConnectionStringBuilder() { DataSource = "book-library.db" }.ToString());
  }

  public DbSet<AuthorItem> Authors { get; set; }
  public DbSet<BookItem> Books { get; set; }
  public DbSet<CategoryItem> Categories { get; set; }
}
