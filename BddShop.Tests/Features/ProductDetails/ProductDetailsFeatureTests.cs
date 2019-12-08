using System;
using BddShop.Features.ProductDetails;
using BddShop.Tests.Infra;
using Xbehave;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Features.ProductDetails
{
    [Collection(TestCollectionNames.WebServer)]
    public class ProductDetailsFeatureTests
    {
        private readonly WebServer _server;

        public ProductDetailsFeatureTests(WebServer server)
        {
            _server = server;
        }

        [Scenario(DisplayName = "Display Product Details")]
        public void DisplayProductDetails(ProductsController sut)
        {
            $"Given I have an instance ProductController"
                .x(() => sut = _server.Services.GetService<ProductsController>());
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
