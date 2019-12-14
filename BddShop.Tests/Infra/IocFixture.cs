using System;
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