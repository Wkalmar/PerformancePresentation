using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Caching.LazyEvaluation;

var summary = BenchmarkRunner.Run<CacheBenchmark>();
Console.ReadLine();

[MemoryDiagnoser]
public class CacheBenchmark
{
    [Benchmark]
    public void EstimateCacheMaxCache()
    {
        const int MaxOperations = 100000;
        var fancy = new Fancy();
        for (var i = 0; i < MaxOperations / 2; i++)
        {
            fancy.Append(100);
        }
        for (var i = 0; i < MaxOperations / 2; i++)
        {
            fancy.GetIndex(i);
        }
    }
}