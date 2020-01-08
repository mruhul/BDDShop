using System;
using System.Collections.Generic;
using System.Text;
using BddShop.Tests.Infra;
using Bolt.RequestBus;
using Xbehave;

namespace BddShop.Tests.Features.Enquiry
{
    public class EnquiryTests : IocFixtureTestBase
    {
        public EnquiryTests(IocFixture fixture) : base(fixture)
        {
        }

        [Scenario(DisplayName = "Send Enquiry Successfully")]
        public void SendEnquirySuccessfully()
        {
            $"Given I have an instance of EnquiryController"
                .x(() => throw new NotImplementedException());
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
