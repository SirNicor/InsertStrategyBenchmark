using Microsoft.Extensions.Configuration;

namespace Runner;

public class AnalyzeAppSettings(IConfiguration config)
{
    public string ConnectionString = config.GetValue<string>("ConnectionString");
}