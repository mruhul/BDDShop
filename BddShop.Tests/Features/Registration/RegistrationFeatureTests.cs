using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BddShop.Features.Registration;
using BddShop.Infra.Adapters;
using BddShop.Infra.Crypto;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xbehave;
using Xunit;

namespace BddShop.Tests.Features.Registration
{
    
    [Collection(TestCollectionNames.WebServer)]
    public class RegistrationFeatureTests
    {
        private readonly WebServer _server;

        public RegistrationFeatureTests(WebServer server)
        {
            _server = server;
        }

        [Scenario(DisplayName = "Registration Successful")]
        public void RegistrationSuccessful(HttpClient client, 
            RegisterUser input,
            HttpResponseMessage registrationRsp,
            UserRecord userRecord)
        {
            $"Given I have an instance of httpclient"
                .x(() => client = _server.CreateClient());
            $"And my registration details are as below"
                .x(() => input = new RegisterUser
                {
                    Email = $"{Guid.NewGuid().ToString()}@test.com",
                    Password = "pass"
                });
            $"When I submit my details for registration"
                .x(async () => registrationRsp = await client.PostFormDataAsync("/accounts/registration", input));
            $"Then I should get a okay response"
                .x(() => { registrationRsp.StatusCode.ShouldBe(HttpStatusCode.OK); });
            $"And my details should be stored"
                .x(() =>
                {
                    userRecord = _server.Services.GetRecordByEmail(input.Email);
                    userRecord.ShouldNotBeNull();
                });
            $"And my password should be stored as hash"
                .x(() => userRecord.PasswordHash.ShouldNotBe(input.Password));
            $"And and email should be sent to my email address"
                .x(() => _server.Services.IsEmailSent(new SendEmailInput
                {
                    TemplateName = "UserRegistration",
                    Subject = "Welcome to BDDshop",
                    To = input.Email
                }).ShouldBeTrue());
            $"And I should be authenticated"
                .x(() => throw new NotImplementedException());
            $"And I should be redirected to home page"
                .x(() => throw new NotImplementedException());
        }
    }
}
