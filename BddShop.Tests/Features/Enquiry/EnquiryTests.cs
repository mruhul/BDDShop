﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BddShop.Features.Enquiry;
using BddShop.Infra.Adapters;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
using Bolt.RequestBus;
using Shouldly;
using Xbehave;
using Xbehave.Execution;

namespace BddShop.Tests.Features.Enquiry
{
    public class EnquiryTests : IocFixtureTestBase
    {
        public EnquiryTests(IocFixture fixture) : base(fixture)
        {
        }

        [Scenario(DisplayName = "Successful Enquiry")]
        [JsonFileData("EnquiryTests/SentSuccessfullyTestInput.json", typeof(SentSuccessfullyTestInput))]
        public void SentSuccessfully(SentSuccessfullyTestInput testInput, 
            IRequestBus bus, 
            ProductRecord stock, 
            SendEnquiryInput input,
            IResponse<SendEnquiryResponse> response)
        {
            var scope = base.Scope();

            $"Given I have an instance of requestbus"
                .x(c => bus = scope.Using(c).RequestBus());
            "And a stock available with id ABC-123"
                .x(c => scope.EnsureProductRecordExists(testInput.GivenExistingProduct));
            "When I submit the user request to send enquiry"
                .x(async () => response = await bus.SendAsync<SendEnquiryInput,SendEnquiryResponse>(testInput.GivenEnquiryInput));
            "Then I should receive a lead id"
                .x(c => response.Result.LeadId.ShouldNotBeNull());
            "And Lead api should receive a request to send following input"
                    .x(c =>
                    {
                        var leadInput = scope.GetProxyInputSent(response.Result.LeadId);
                        testInput.ExpectedProxyInputSentToLeadApi.Id = leadInput.Id; // id is autogenerated
                        leadInput.ToJson().ShouldBe(testInput.ExpectedProxyInputSentToLeadApi.ToJson());
                    });
            "And Email should sent with correct Input"
                .x(() =>
                {
                    var emailSentDto = scope.FindEmailSent(testInput.GivenExistingProduct.SellerEmail).ToArray();
                    emailSentDto.Length.ShouldBe(1);
                    emailSentDto.First().ShouldBeSameContent(testInput.ExpectedSendEmailInput);
                });
        }

        public class SentSuccessfullyTestInput
        {
            public ProductRecord GivenExistingProduct { get; set; }
            public SendEnquiryInput GivenEnquiryInput { get; set; }
            public LeadProxyInput ExpectedProxyInputSentToLeadApi { get; set; }
            public SendEmailInput ExpectedSendEmailInput { get; set; }
        }
    }
}