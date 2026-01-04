using Bogus;
using BookLibraryAPI.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookLibraryAPI.Infra.Data;

public static class InitialiserExtensions
{
  public static async Task InitialiseDatabaseAsync(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();

    var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();

    await initialiser.InitialiseAsync();
    await initialiser.SeedData();
  }
}

public class AppDbContextInitialiser
{
  private readonly ILogger<AppDbContextInitialiser> _logger;
  private readonly AppDbContext _context;

  public AppDbContextInitialiser(ILogger<AppDbContextInitialiser> logger, AppDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task InitialiseAsync()
  {
    try
    {
      await _context.Database.EnsureDeletedAsync();
      await _context.Database.EnsureCreatedAsync();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while initialising the database.");
      throw;
    }
  }

  public async Task SeedData()
  {
    try
    {
      var authors = await SeedAuthorsAsync();
      var categories = await SeedCategoriesAsync();
      await SeedBooksAsync(authors, categories);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while seeding the database.");
      throw;
    }
  }

  private async Task<List<AuthorItem>> SeedAuthorsAsync()
  {
    var authorFaker = new Faker<AuthorItem>()
      .RuleFor(a => a.Name, f => f.Name.FullName())
      .RuleFor(a => a.Bio, (f, a) => f.Random.Bool(0.8f) ? f.Lorem.Paragraph() : null)
      .RuleFor(a => a.CreatedAt, f => f.Date.Past(5).ToUniversalTime())
      .RuleFor(
        a => a.UpdatedAt,
        (f, a) => f.Random.Bool(0.85f) ? f.Date.Between(a.CreatedAt, DateTime.UtcNow).ToUniversalTime() : null
      );

    var authors = authorFaker.Generate(100);
    _context.Authors.AddRange(authors);
    await _context.SaveChangesAsync();

    return authors;
  }

  private async Task<List<CategoryItem>> SeedCategoriesAsync()
  {
    var categoryFaker = new Faker<CategoryItem>()
      .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
      .RuleFor(c => c.Description, (f, c) => f.Random.Bool(0.85f) ? f.Lorem.Sentence(10) : null)
      .RuleFor(c => c.CreatedAt, f => f.Date.Past(3).ToUniversalTime())
      .RuleFor(
        c => c.UpdatedAt,
        (f, c) => f.Random.Bool(0.85f) ? f.Date.Between(c.CreatedAt, DateTime.UtcNow).ToUniversalTime() : null
      );

    var categories = categoryFaker.Generate(25);
    _context.Categories.AddRange(categories);
    await _context.SaveChangesAsync();

    return categories;
  }

  private async Task SeedBooksAsync(List<AuthorItem> authors, List<CategoryItem> categories)
  {
    var bookFaker = new Faker<BookItem>()
      .RuleFor(b => b.Title, f => f.Lorem.Sentence(3, 5).TrimEnd('.'))
      .RuleFor(b => b.ISBN, (f, b) => f.Random.Bool(0.75f) ? f.Commerce.Ean13() : null)
      .RuleFor(b => b.PublishedAt, f => f.Date.Past(30).ToUniversalTime())
      .RuleFor(b => b.AuthorId, f => f.PickRandom(authors).Id)
      .RuleFor(b => b.CategoryId, (f, b) => f.Random.Bool(0.85f) ? f.PickRandom(categories).Id : null)
      .RuleFor(b => b.IsRead, f => f.Random.Bool(0.3f))
      .RuleFor(b => b.CreatedAt, f => f.Date.Past(2).ToUniversalTime())
      .RuleFor(
        b => b.UpdatedAt,
        (f, b) => f.Random.Bool(0.85f) ? f.Date.Between(b.CreatedAt, DateTime.UtcNow).ToUniversalTime() : null
      );

    var books = bookFaker.Generate(1000);
    _context.Books.AddRange(books);
    await _context.SaveChangesAsync();
  }
}
