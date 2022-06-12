// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Directory.EnumerateFiles.Batching;

BenchmarkRunner.Run<BatchingBenchmark>();
Console.ReadLine();

public class BatchingBenchmark
{
    [Benchmark]
    public async Task WorkerBenchmark()
    {
        await new Worker().DoWork();
    }

    [Benchmark]
    public async Task WorkerImprovedBenchmark()
    {
        await new WorkerImproved().DoWork();
    }
}