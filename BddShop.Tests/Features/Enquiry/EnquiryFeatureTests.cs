using System;
using System.Collections.Generic;
using System.Text;
using BddShop.Features.Enquiry;
using BddShop.Infra.Adapters;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
using Bolt.RequestBus;
using Xbehave;

namespace BddShop.Tests.Features.Enquiry
{
    public class EnquiryFeatureTests : IocFixtureTestBase
    {
        public EnquiryFeatureTests(IocFixture fixture) : base(fixture)
        {
        }

        [Scenario(DisplayName = "Send Enquiry Successfully")]
        public void SendEnquirySuccessfully(EnquiryController sut, SendEnquiryRequest request)
        {
            var bus = GetService<IRequestBus>();

            $"Given I have an instance of EnquiryController"
                .x(() => sut = GetService<EnquiryController>());
            $"And a stock available in our system"
                .x(() => ServiceProvider.EnsureProductRecordExists(new ProductRecord
                {
                    Id = "abcd-123",
                    Title = "title 1",
                    Price = 99.45m
                }));
            $"And I have an instance of enquiry input"
                .x(() => request = new SendEnquiryRequest
                {
                    Email = "test@gmail.com",
                    NetworkId = "abcd-123"
                });
            $"When I sent the enquiry"
                .x(() => throw new NotImplementedException());
            $"Then I should get a response"
                .x(() => throw new NotImplementedException());
            $"And the response should be successful"
                .x(() => throw new NotImplementedException());
            "And a lead should be sent as below"
                .x(() => throw new NotImplementedException());
            "And an email should be sent to the seller"
                .x(() => throw new NotImplementedException());
            "And a confirmation email should be sent to the buyer"
                .x(() => throw new NotImplementedException());

        }
    }
}
