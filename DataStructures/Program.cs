// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using DataStructures;

BenchmarkRunner.Run<DataStructuresBenchmark>();
Console.ReadLine();

public class DataStructuresBenchmark
{
    private readonly RuleEngine _ruleEngine;
    private readonly DataStorage _storage;

    public DataStructuresBenchmark()
    {
        _ruleEngine = new RuleEngine();
        _storage = new DataStorage();
    }

    [Benchmark]
    public void Array()
    {
        var items = _storage.GetAll();
        _ruleEngine.ApplyRules(ref items);
    }

    [Benchmark]
    public void Dictionary()
    {
        var items = _storage.GetAll().ToDictionary(k => k.Key, v => v);
        _ruleEngine.ApplyRules(ref items);
        var result = items.ToList();
    }
}