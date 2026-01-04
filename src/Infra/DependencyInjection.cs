using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Infra.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
  public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
  {
    var connectionString = new SqliteConnectionStringBuilder() { DataSource = "book-library.db" }.ToString();
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
    builder.Services.AddScoped<IAppDbContext, AppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
    builder.Services.AddScoped<AppDbContextInitialiser>();
  }
}
