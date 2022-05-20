// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using OrleansPlayground.Grains;

BenchmarkRunner.Run<SomeBenchmark>();

[MemoryDiagnoser]
public class SomeBenchmark
{
    private readonly ThingToBenchmark _thing;

    public SomeBenchmark()
    {
        _thing = new ThingToBenchmark();
    }

    [Benchmark]
    public void One() => _thing.Foo(Random.Shared.Next(0), Random.Shared.Next(1000));
}