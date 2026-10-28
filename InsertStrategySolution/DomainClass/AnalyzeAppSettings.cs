using Microsoft.Extensions.Configuration;

namespace DomainClass;

public static class AnalyzeAppSettings
{
    private static readonly Lazy<IConfiguration> Config = new(Build);
    private static IConfiguration Instance => Config.Value;
    public static string GetConnectionString(string name = "DefaultConnection")
        => Instance.GetConnectionString(name)
           ?? throw new InvalidOperationException($"Connection string '{name}' not found");

    public static T Get<T>(string key) where T : notnull
        => Instance.GetSection(key).Get<T>()
           ?? throw new InvalidOperationException($"Section '{key}' not found");

    private static IConfiguration Build()
        => new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();
}