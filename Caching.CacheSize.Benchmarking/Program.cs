using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Caching.CacheSize;

var summary = BenchmarkRunner.Run<CacheBenchmark>();
Console.ReadLine();

[MemoryDiagnoser]
public class CacheBenchmark
{
    [Benchmark]
    public void EstimateCacheFor3Years()
    {
        var cache = new ProcessedFilesCache();
        const int numberOfEntries = 1700000;//50000 per day * 365 * 3
        for (int i = 0; i < numberOfEntries; i++)
        {
            cache.Add(Guid.NewGuid().ToString());
        }
    }
}