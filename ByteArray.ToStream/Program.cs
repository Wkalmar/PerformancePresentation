// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using ByteArray.ToStream;

BenchmarkRunner.Run<HasingBenchmark>();
Console.ReadLine();

[MemoryDiagnoser]
public class HasingBenchmark
{
    private const string path = @"D:\Programming\batching\0000f821-a001-47e1-a56c-888ded5f6c99.wav";
    
    [Benchmark]
    public void ByteArray()
    {
        var bytes = File.ReadAllBytes(path);
        HashUtils.ComputeHash(bytes);
    }

    [Benchmark]
    public void Stream()
    {
        using var stream = File.OpenRead(path);
        HashUtils.ComputeStream(stream);
    }
}
