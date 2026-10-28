using BenchmarkDotNet.Running;
using Benchmarks;

public static class StartBenchmark
{
    public static void Main()
    {
        // var summaries = BenchmarkSwitcher
        //     .FromTypes(new[] { typeof(InsertBenchmarks_Clean) })   
        //     .Run(args, new BenchmarkConfig());                   
        //
        // return summaries.Any(s => s.Reports.Any(r => !r.Success)) ? 1 : 0;
    }
}