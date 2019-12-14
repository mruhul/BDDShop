using BddShop.Features.Home.Showroom;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
using Bolt.RequestBus;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
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
        [ClassData(typeof(ShowroomTestData))]
        public void PresentShowroomSection(ShowroomTestDataInput input, IRequestBus bus, IResponse<ShowroomViewModel> rsp)
        {
            $"Given current tenant is {input.Tenant}"
                .x(() => _scope.ServiceProvider.SetCurrentTenant(input.Tenant));
            $"And I have an instance of request bus"
                .x(() => bus = _scope.ServiceProvider.GetService<IRequestBus>());
            $"When I sent request for showroom view model"
                .x(async () => rsp = await bus.SendAsync<ShowroomRequest,ShowroomViewModel>(new ShowroomRequest()));
            $"Then I should receive a showroom view model response"
                .x(() => rsp.IsSucceed.ShouldBeTrue());
            $"And view model should not be null"
                .x(() => rsp.Result.ShouldNotBeNull());
            $"And view model title should be correct"
                .x(() => rsp.Result.Heading.ShouldBe(input.ExpectedViewModel.Heading));
            $"And view model should have a link to view all"
                .x(() => rsp.Result.ViewAllLink.ShouldNotBeNull());
            $"And view model view all link should have correct text"
                .x(() => rsp.Result.ViewAllLink.Text.ShouldBe(input.ExpectedViewModel.ViewAllLink.Text));
            $"And view model view all link should have correct url"
                .x(() => rsp.Result.ViewAllLink.Url.ShouldBe(input.ExpectedViewModel.ViewAllLink.Url));

        }

        public class ShowroomTestDataInput
        {
            public string Tenant { get; set; }
            public ShowroomViewModel ExpectedViewModel { get; set; }
        }

        public class ShowroomTestData : TheoryData<ShowroomTestDataInput>
        {
            public ShowroomTestData()
            {
                Add(new ShowroomTestDataInput
                {
                    Tenant = "carsales",
                    ExpectedViewModel = new ShowroomViewModel
                    {
                        Heading = "New Car Showroom",
                        ViewAllLink = new HtmlLink
                        {
                            Url = "https://www.carsales.com.au/new-cars/#start-search",
                            Text = "View all body types"
                        }
                    }
                });
                Add(new ShowroomTestDataInput
                {
                    Tenant = "bikesales",
                    ExpectedViewModel = new ShowroomViewModel
                    {
                        Heading = "New Bike Showroom",
                        ViewAllLink = new HtmlLink
                        {
                            Url = "https://www.bikesales.com.au/new-bikes/#start-search",
                            Text = "View all body types"
                        }
                    }
                });
            }
        }
    }
}
