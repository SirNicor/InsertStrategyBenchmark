using Microsoft.Extensions.Configuration;
using Runner;

public static class Programm
{
    public static void Main()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        IConfiguration configuration = builder.Build();
        AnalyzeAppSettings analyzeAppSettings = new AnalyzeAppSettings(configuration);
    }
}