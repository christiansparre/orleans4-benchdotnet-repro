using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using Orleans.Hosting;

var builder = Host.CreateDefaultBuilder();

builder.UseOrleans(silo =>
{
    silo.UseLocalhostClustering();
    silo.Configure<ClusterOptions>(options =>
    {
        options.ClusterId = "Local";
        options.ServiceId = "Playground";
    });
});

var host = builder.Build();

await host.RunAsync();
