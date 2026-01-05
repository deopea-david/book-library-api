using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
  public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
  {
    var connectionString = builder.Configuration.GetConnectionString("BookLibrary");
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new("connection string missing");
    }

    if (builder.Environment.IsDevelopment())
    {
      builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
    }
    else
    {
      builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
    }

    builder.Services.AddScoped<IAppDbContext, AppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
    builder.Services.AddScoped<AppDbContextInitialiser>();
  }
}
