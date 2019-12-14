using System;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Xbehave;
using Xunit;

namespace BddShop.Tests.Features.Home.Showroom
{
    [Collection(TestCollectionNames.IocFixture)]
    public class ShowroomFeatureTests
    {
        private readonly IServiceScope _scope;

        public ShowroomFeatureTests(IocFixture fixture)
        {
            _scope = fixture.Scope();
        }

        ~ShowroomFeatureTests()
        {
            _scope.Dispose();
        }

        [Scenario(DisplayName = "PresentShowroomViewModel")]
        public void PresentShowroomSection()
        {
            $"Given current tenant is carsales"
                .x(() => _scope.ServiceProvider.SetCurrentTenant("carlsales"));
            $"And I have an instance of request bus"
                .x(() => throw new NotImplementedException());
            $"When I send request for showroom view model"
                .x(() => throw new NotImplementedException());
            $"Then I should receive a showroom view model"
                .x(() => throw new NotImplementedException());
            $"And view model should match as expected"
                .x(() => throw new NotImplementedException());
        }
    }
}
