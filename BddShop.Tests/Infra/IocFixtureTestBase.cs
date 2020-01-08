using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BddShop.Tests.Infra
{
    [Collection(TestCollectionNames.IocFixture)]
    public abstract class IocFixtureTestBase
    {
        private readonly IServiceScope _scope;

        protected IocFixtureTestBase(IocFixture fixture)
        {
            _scope = fixture.Scope();
        }

        protected IServiceProvider ServiceProvider => _scope.ServiceProvider;
        protected T GetService<T>() => _scope.ServiceProvider.GetService<T>();
        protected IEnumerable<T> GetServices<T>() => _scope.ServiceProvider.GetServices<T>();

        ~IocFixtureTestBase() => _scope.Dispose();
    }
}
