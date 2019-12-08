using System;
using System.Collections.Generic;
using System.Text;
using BddShop.Features.ProductDetails;
using BddShop.Tests.Infra;
using Xbehave;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace BddShop.Tests.Features.ProductDetails
{
    [Collection(TestCollectionNames.WebServer)]
    public class DiscountCalculatorTests
    {
        private readonly WebServer _server;

        public DiscountCalculatorTests(WebServer server)
        {
            _server = server;
        }

        [Trait("Category", TestCategoryNames.Fast)]
        [Scenario(DisplayName = "Calculate Discount Correctly")]
        [InlineData(0,0)]
        [InlineData(9,0)]
        [InlineData(10,1)]
        [InlineData(20,1)]
        [InlineData(49,1)]
        [InlineData(50,2)]
        [InlineData(99,2)]
        [InlineData(100,5)]
        [InlineData(200,5)]
        public void CalculateDiscountCorrectly(decimal price, 
            decimal expectedDiscount,
            DiscountCalculator sut, 
            decimal result)
        {
            $"Given I have an instance of price calculator"
                .x(() => sut = _server.Services.GetService<DiscountCalculator>());
            $"When I run the calculator for price {price}"
                .x(() => result = sut.Calculate(price));
            $"Then I should get discount value {expectedDiscount}"
                .x(() => result.ShouldBe(expectedDiscount));
        }
    }
}
