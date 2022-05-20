using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Orleans;
using OrleansPlayground.Grains.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace OrleansPlayground.Tests;


[Collection(TestClusterFixture.CollectionName)]
public class HelloWorldTests
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly IClusterClient _client;

    public HelloWorldTests(TestClusterFixture fixture, ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
        _client = fixture.Cluster.Client;
    }

    [Fact]
    public async Task Test1()
    {
        var message = await _client.GetGrain<IHelloWorldGrain>("hello-test").Hello("Jane Doe");
    }

    [Fact]
    public async Task Test2()
    {
        var grains = Enumerable.Range(1, 1000).Select(i => _client.GetGrain<IHelloWorldGrain>($"hello-test-{i}")).ToArray();

        var tasks = Enumerable.Range(1, 10000).Select(_ => grains[Random.Shared.Next(0, grains.Length)].Hello($"Person {_}")).ToArray();

        var timer = Stopwatch.StartNew();
        await Task.WhenAll(tasks);
        timer.Stop();
        
        _outputHelper.WriteLine($"Said Hello {tasks.Length} timer in {timer.Elapsed:g}. That is {tasks.Length/timer.Elapsed.TotalSeconds:N0} hello's per second!");

        _outputHelper.WriteLine("Doing it again!");

        var tasks2 = Enumerable.Range(1, 10000).Select(_ => grains[Random.Shared.Next(0, grains.Length)].Hello($"Person {_}")).ToArray();

        var timer2 = Stopwatch.StartNew();
        await Task.WhenAll(tasks2);
        timer2.Stop();

        _outputHelper.WriteLine($"Said Hello {tasks2.Length} timer in {timer2.Elapsed:g}. That is {tasks2.Length / timer2.Elapsed.TotalSeconds:N0} hello's per second!");
    }
}