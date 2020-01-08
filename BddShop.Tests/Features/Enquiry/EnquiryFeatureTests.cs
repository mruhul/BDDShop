using System;
using System.Collections.Generic;
using System.Text;
using BddShop.Features.Enquiry;
using BddShop.Infra.Adapters;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
using Bolt.RequestBus;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xbehave;

namespace BddShop.Tests.Features.Enquiry
{
    public class EnquiryFeatureTests : IocFixtureTestBase
    {
        public EnquiryFeatureTests(IocFixture fixture) : base(fixture)
        {
        }

        [Scenario(DisplayName = "Send Enquiry Successfully")]
        public void SendEnquirySuccessfully(EnquiryController sut, SendEnquiryRequest request, SendEnquiryResponse response)
        {
            var bus = GetService<IRequestBus>();
            var stock = new ProductRecord
            {
                Id = "abcd-123",
                Title = "title 1",
                Price = 99.45m
            };

            $"Given I have an instance of EnquiryController"
                .x(() => sut = GetService<EnquiryController>());
            $"And a stock available in our system"
                .x(() => ServiceProvider.EnsureProductRecordExists(stock));
            $"And I have an instance of enquiry input"
                .x(() => request = new SendEnquiryRequest
                {
                    Email = "test@gmail.com",
                    NetworkId = stock.Id
                });
            $"When I sent the enquiry"
                .x(async () =>
                {
                    var okResult = (await sut.Post(request)) as OkObjectResult;
                    response = okResult.Value as SendEnquiryResponse;
                });
            $"Then I should get a response"
                .x(() => response.ShouldNotBeNull());
            $"And the response should have lead id"
                .x(() => response.LeadId.ShouldNotBeNull());
            "And a lead should be sent as below"
                .x(() => throw new NotImplementedException());
            "And an email should be sent to the seller"
                .x(() => throw new NotImplementedException());
            "And a confirmation email should be sent to the buyer"
                .x(() => throw new NotImplementedException());

        }
    }
}
