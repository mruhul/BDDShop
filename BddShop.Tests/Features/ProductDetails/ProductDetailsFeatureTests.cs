using System;
using BddShop.Features.ProductDetails;
using BddShop.Tests.Infra;
using Microsoft.AspNetCore.Mvc;
using Xbehave;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

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
        public void DisplayProductDetails(ProductsController sut, LoadProductDetails input, ProductDetailsViewModel vm)
        {


            $"Given I have an instance ProductController"
                .x(() => sut = _server.Services.GetService<ProductsController>());
            $"And I have an input to load product details"
                .x(() => input = new LoadProductDetails
                {
                    Id = Guid.NewGuid().ToString()
                });
            $"When I request to get product details"
                .x(() => { vm = sut.Get(input).ViewModel<ProductDetailsViewModel>(); });
            $"Then I should get a product viewmodel"
                .x(() => vm.ShouldNotBeNull());
            $"And I should see correct title of the product"
                .x(() => throw new NotImplementedException());
            $"And I should see correct price of the product"
                .x(() => throw new NotImplementedException());
            $"And I should see discount applied to the product"
                .x(() => throw new NotImplementedException());
        }
    }
}
