using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BddShop.Tests.Infra
{
    public class IocFixture
    {
        private readonly IServiceProvider _sp;

        public IocFixture()
        {
            var sc = new ServiceCollection();

            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", true)
                        .Build();
            
            sc.AddLogging();
            
            sc.AddSingleton<IConfiguration>(config);
            
            new ServiceConfiguration().Configure(sc);

            FakeServiceSetup.Run(sc, true);

            _sp = sc.BuildServiceProvider();
        }

        public IServiceScope Scope() => _sp.CreateScope();
    }

    [CollectionDefinition(TestCollectionNames.IocFixture)]
    public class IocFixtureXUnitCollection : ICollectionFixture<IocFixture>
    {}
}