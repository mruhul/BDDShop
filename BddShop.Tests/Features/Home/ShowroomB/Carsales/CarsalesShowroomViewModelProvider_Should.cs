using BddShop.Features.Home.Showroom.Carsales;
using Shouldly;
using Xunit;

namespace BddShop.Tests.Features.Home.ShowroomB.Carsales
{
    public class CarsalesShowroomViewModelProvider_Should
    {
        [Fact]
        public void Return_Correct_ViewModel()
        {
            // Arrange
            var sut = new CarsalesShowroomViewModelProvider();

            // Act
            var vm = sut.Get();

            // Assert
            vm.Heading.ShouldBe("New Car Showroom");
            vm.ViewAllLink.ShouldNotBeNull();
            vm.ViewAllLink.Url.ShouldBe("https://www.carsales.com.au/new-cars/#start-search");
            vm.ViewAllLink.Text.ShouldBe("View all body types");
        }
    }
}
