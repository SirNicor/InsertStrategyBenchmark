using Microsoft.Extensions.Configuration;

namespace DomainClass;

public class AnalyzeAppSettings(IConfiguration config)
{
    public string ConnectionString = config.GetValue<string>("ConnectionString");
}