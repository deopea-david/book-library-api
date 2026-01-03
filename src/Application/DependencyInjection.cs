using BookLibraryAPI.Application.Author;
using BookLibraryAPI.Application.Book;
using BookLibraryAPI.Application.Category;
using BookLibraryAPI.Application.Common.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
  public static void AddApplicationServices(this IHostApplicationBuilder builder)
  {
    // Register services with the DI container
    builder.Services.AddScoped<IAuthorService, AuthorService>();
    builder.Services.AddScoped<IBookService, BookService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
  }
}
