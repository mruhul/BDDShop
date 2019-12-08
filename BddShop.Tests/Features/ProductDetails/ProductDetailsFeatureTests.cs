using System;
using Xbehave;

namespace BddShop.Tests.Features.ProductDetails
{
    public class ProductDetailsFeatureTests
    {
        [Scenario(DisplayName = "Display Product Details")]
        public void DisplayProductDetails()
        {
            $"Given I have an instance ProductController"
                .x(() => throw new NotImplementedException());
            $"When I request to get product details"
                .x(() => throw new NotImplementedException());
            $"Then I should get a product viewmodel"
                .x(() => throw new NotImplementedException());
            $"And I should see correct title of the product"
                .x(() => throw new NotImplementedException());
            $"And I should see correct price of the product"
                .x(() => throw new NotImplementedException());
            $"And I should see discount applied to the product"
                .x(() => throw new NotImplementedException());
        }
    }
}
