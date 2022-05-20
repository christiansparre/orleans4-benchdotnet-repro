﻿using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using OrleansPlayground.Grains.Interfaces;

namespace OrleansPlayground.Grains;

public class HelloWorldGrain : IHelloWorldGrain, IGrainBase
{
    private readonly ILogger<HelloWorldGrain> _logger;

    public HelloWorldGrain(IGrainContext grainContext, ILogger<HelloWorldGrain> logger)
    {
        _logger = logger;
        GrainContext = grainContext;
    }

    public Task<HelloMessage> Hello(string name)
    {
        _logger.Info("Received hello call from {Name}", name);

        return Task.FromResult(new HelloMessage($"Hello {name}, {this.GetPrimaryKeyString()} says hi! 👋", DateTimeOffset.UtcNow));
    }

    public Task OnActivateAsync(CancellationToken token)
    {
        _logger.Info("Hello grain {GrainId} was activated", GrainContext.GrainId);
        return Task.CompletedTask;
    }

    public IGrainContext GrainContext { get; }
}