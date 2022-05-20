using System;
using Microsoft.Extensions.Configuration;
using Orleans;
using Orleans.Hosting;
using Orleans.TestingHost;
using Xunit;

namespace OrleansPlayground.Tests;

public class TestClusterFixture : IDisposable
{
    public const string CollectionName = "TestClusterFixtureCollection";

    [CollectionDefinition(CollectionName)]
    public class TestClusterFixtureCollection : ICollectionFixture<TestClusterFixture>
    {
    }

    public TestClusterFixture()
    {
        var builder = new TestClusterBuilder();
        builder.AddSiloBuilderConfigurator<TestSiloConfigurator>();
        builder.AddClientBuilderConfigurator<TestClientConfigurator>();

        Cluster = builder.Build();
        Cluster.Deploy();
    }

    public void Dispose()
    {
        Cluster.StopAllSilos();
    }

    public TestCluster Cluster { get; private set; }

    public class TestSiloConfigurator : ISiloConfigurator
    {
        public void Configure(ISiloBuilder builder)
        {
            builder.ConfigureServices(services => { });
        }
    }

    public class TestClientConfigurator : IClientBuilderConfigurator
    {
        public void Configure(IConfiguration configuration, IClientBuilder clientBuilder)
        {

        }
    }

}