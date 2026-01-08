namespace BookLibraryAPI.Infra.Options;

public class CacheOptions
{
  public const string Key = "Cache";

  public string Host { get; set; } = string.Empty;
  public int? Port { get; set; } = null;
}
