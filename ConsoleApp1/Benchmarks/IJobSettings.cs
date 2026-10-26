using BenchmarkDotNet.Jobs;

namespace Benchmarks;

public interface IJobSettings
{
    public Job ReturnJob();
}