using BenchmarkDotNet.Jobs;

namespace Benchmarks;

public class JobSettingsEFCoreStrategy : IJobSettings
{
    public Job ReturnJob()
    {
        throw new NotImplementedException();
    }
}