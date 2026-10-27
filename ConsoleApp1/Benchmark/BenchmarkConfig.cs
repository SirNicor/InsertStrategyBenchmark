using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using Perfolizer.Mathematics.OutlierDetection;

namespace Benchmarks;

public class BenchmarkConfig : ManualConfig
{
    public BenchmarkConfig()
    {
        AddJob(
            Job.Default                                          
                .WithId("DBSteadyState")                        
                .WithStrategy(RunStrategy.Throughput)
                .WithJit(Jit.RyuJit)
                .WithLaunchCount(1)                         
                .WithWarmupCount(2)                         
                .WithIterationCount(10)                       
                .WithInvocationCount(1)                      
                .WithUnrollFactor(1)                         
                .WithOutlierMode(OutlierMode.RemoveUpper)
                .WithGcServer(true)
        );
        WithArtifactsPath(Path.Combine(
            Environment.CurrentDirectory,
            "ResultTests",
            DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")));
        AddExporter(MarkdownExporter.GitHub);
        AddDiagnoser(MemoryDiagnoser.Default);
        AddExporter(CsvExporter.Default);
        Options |= ConfigOptions.Default;
        
    }
}