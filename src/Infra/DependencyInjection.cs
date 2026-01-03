using BookLibraryAPI.Infra.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static partial class DependencyInjection
{
  public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
  {
    builder.Services.AddDbContext<SQLiteContext>();
  }
}
