using System;
using System.Collections.Generic;
using System.Text;
using BddShop.Features.Enquiry;
using BddShop.Tests.Infra;
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
        public void SendEnquirySuccessfully(EnquiryController sut)
        {
            var bus = GetService<IRequestBus>();

            $"Given I have an instance of EnquiryController"
                .x(() => sut = GetService<EnquiryController>());
            $"And I have an instance of enquiry input"
                .x(() => throw new NotImplementedException());
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
