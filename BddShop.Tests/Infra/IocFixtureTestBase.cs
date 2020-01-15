using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BddShop.Tests.Infra
{
    [Collection(TestCollectionNames.IocFixture)]
    public abstract class IocFixtureTestBase
    {
        private readonly IocFixture _fixture;

        protected IocFixtureTestBase(IocFixture fixture)
        {
            _fixture = fixture;
        }

        protected IServiceScope Scope() => _fixture.Scope();
    }
}
