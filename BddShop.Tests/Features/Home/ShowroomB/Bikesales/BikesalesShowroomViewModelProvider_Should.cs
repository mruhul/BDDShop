using BddShop.Features.Home.Showroom.Bikesales;
using Shouldly;
using Xunit;

namespace BddShop.Tests.Features.Home.ShowroomB.Bikesales
{
    public class BikesalesShowroomViewModelProvider_Should
    {
        [Fact]
        public void Return_Correct_ViewModel()
        {
            // Arrange
            var sut = new BikesalesShowroomViewModelProvider();

            // Act
            var vm = sut.Get();

            // Assert
            vm.Heading.ShouldBe("New Bike Showroom");
            vm.ViewAllLink.ShouldNotBeNull();
            vm.ViewAllLink.Url.ShouldBe("https://www.bikesales.com.au/new-bikes/#start-search");
            vm.ViewAllLink.Text.ShouldBe("View all body types");
        }
    }
}
