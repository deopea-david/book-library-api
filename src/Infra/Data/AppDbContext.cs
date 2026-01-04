using System.Reflection;
using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Domain.Entities;
using BookLibraryAPI.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPI.Infra.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorItemConfiguration).Assembly);
  }

  public DbSet<AuthorItem> Authors { get; set; }
  public DbSet<BookItem> Books { get; set; }
  public DbSet<CategoryItem> Categories { get; set; }
}
