using System.Threading.Tasks;
using BddShop.Features.Home.Showroom;
using BddShop.Infra.Adapters;
using Bolt.RequestBus;
using Shouldly;
using Xunit;

namespace BddShop.Tests.Features.Home.ShowroomB
{
    public class ShowroomRequestHandler_Should
    {
        [Fact]
        public async Task Return_Correct_ViewModel()
        {
            // Arrange
            var sut = new ShowroomRequestHandler(
                new TestTenant("carsales"),
                new []{new TestShowroomViewModelProvider("carsales", new ShowroomViewModel
                {
                    Heading = "test heading",
                    ViewAllLink = new HtmlLink
                    {
                        Text = "test link text",
                        Url = "test url"
                    }
                })}
                ) as IRequestHandlerAsync<ShowroomRequest, ShowroomViewModel>;

            // Act
            var rsp = await sut.Handle(null, new ShowroomRequest());

            // Assert
            rsp.ShouldNotBeNull();
            rsp.IsSucceed.ShouldBeTrue();
            rsp.Result.ShouldNotBeNull();
            rsp.Result.Heading.ShouldBe("test heading");
            rsp.Result.ViewAllLink.ShouldNotBeNull();
            rsp.Result.ViewAllLink.Text.ShouldBe("test link text");
            rsp.Result.ViewAllLink.Url.ShouldBe("test url");
        }
    }

    public class TestTenant : ITenant
    {
        public TestTenant(string tenantName)
        {
            Name = tenantName;
        }

        public string Name { get; }
    }

    public class TestShowroomViewModelProvider : IShowroomViewModelProvider
    {
        private readonly string _tenant;
        private readonly ShowroomViewModel _vm;

        public TestShowroomViewModelProvider(string tenant, ShowroomViewModel vm)
        {
            _tenant = tenant;
            _vm = vm;
        }

        public string[] ForTenants => new[] {_tenant};
        public ShowroomViewModel Get()
        {
            return _vm;
        }
    }
}
