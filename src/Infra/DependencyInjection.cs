using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Infra.Data;
using BookLibraryAPI.Infra.Options;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

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
      builder.Services.AddEFSecondLevelCache(options =>
        options
          .UseMemoryCacheProvider()
          .ConfigureLogging(true)
          .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromSeconds(30))
      );
      builder.Services.AddDbContext<AppDbContext>(
        (sp, options) =>
          options.UseSqlite(connectionString).AddInterceptors(sp.GetRequiredService<SecondLevelCacheInterceptor>())
      );
    }
    else
    {
      var cacheOptions = new CacheOptions();
      builder.Configuration.GetSection(CacheOptions.Key).Bind(cacheOptions);
      builder.Services.AddEFSecondLevelCache(options =>
        options
          .UseStackExchangeRedisCacheProvider(
            new ConfigurationOptions
            {
              EndPoints = { { cacheOptions.Host ?? "127.0.0.1", cacheOptions.Port ?? 6379 } },
              ConnectTimeout = 10000,
            },
            TimeSpan.FromMinutes(1)
          )
          .ConfigureLogging(true)
          .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromSeconds(10))
      );

      builder.Services.AddDbContext<AppDbContext>(
        (sp, options) =>
          options.UseNpgsql(connectionString).AddInterceptors(sp.GetRequiredService<SecondLevelCacheInterceptor>())
      );
    }

    builder.Services.AddScoped<IAppDbContext, AppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
    builder.Services.AddScoped<AppDbContextInitialiser>();
  }
}
