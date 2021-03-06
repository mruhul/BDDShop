﻿using System;
using BddShop.Features.ProductDetails;
using BddShop.Infra.Adapters;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
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

        [Trait("Category", TestCategoryNames.Fast)]
        [Scenario(DisplayName = "Display Product Details")]
        public void DisplayProductDetails(IServiceScope scope, ProductsController sut, 
            LoadProductDetails input, 
            ProductDetailsViewModel vm,
            ProductRecord record)
        {
            scope = _server.Services.CreateScope();
            record = new ProductRecord
            {
                Id = Guid.NewGuid().ToString(),
                Price = 20,
                Title = "BDD TShirt"
            };

            $"Given The system has an existing product record"
                .x(c => scope.Using(c).ServiceProvider.EnsureProductRecordExists(record));
            $"Given I have an instance ProductController"
                .x(() => sut = scope.ServiceProvider.GetService<ProductsController>());
            $"And I have an input to load product details"
                .x(() => input = new LoadProductDetails
                {
                    Id = record.Id
                });
            $"When I request to get product details"
                .x(async () => { vm = (await sut.Get(input)).ViewModel<ProductDetailsViewModel>(); });
            $"Then I should get a product viewmodel"
                .x(() => vm.ShouldNotBeNull());
            $"And I should see correct title of the product"
                .x(() => vm.Title.ShouldBe(record.Title));
            $"And I should see correct price of the product"
                .x(() => vm.PriceText.ShouldBe("$20.00"));
            $"And I should see discount offer to the product"
                .x(() => vm.DiscountText.ShouldBe("$1.00"));
        }
    }
}
