namespace Benchmarks;

public abstract class TimedBenchmarksBase
{
    private static readonly TimeSpan PerIterationTimeout = TimeSpan.FromMinutes(10);

    protected async Task RunWithTimeout(Func<CancellationToken, Task> body)
    {
        using var cts = new CancellationTokenSource(PerIterationTimeout);
        await body(cts.Token);   
    }
}