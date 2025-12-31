using BookLibraryAPI.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Data;

public class SQLiteContext(DbContextOptions<SQLiteContext> options) : DbContext(options)
{
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite(new SqliteConnectionStringBuilder() { DataSource = "book-library.db" }.ToString());
  }

  public DbSet<Author> Authors { get; set; }
  public DbSet<Book> Books { get; set; }
  public DbSet<Category> Categories { get; set; }
}
