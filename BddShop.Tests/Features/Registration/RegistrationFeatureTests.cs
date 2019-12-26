using System;
using System.Net;
using System.Net.Http;
using BddShop.Features.Registration;
using BddShop.Infra;
using BddShop.Infra.Adapters;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
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
        
        [Trait("Category", TestCategoryNames.Fast)]
        [Scenario(DisplayName = "Registration Page Access")]
        public void RegistrationPageAccess(HttpClient client, 
            HttpResponseMessage rsp)
        {
            $"Given I have an instance of httpclient"
                .x(() => client = _server.Http());
            $"When I visit registration page"
                .x(async () => rsp = await client.GetAsync("accounts/registration"));
            $"Then I should get okay status code"
                .x(() => rsp.StatusCode.ShouldBe(HttpStatusCode.OK));
        }
        
        [Trait("Category", TestCategoryNames.Fast)]
        [Scenario(DisplayName = "Registration Successful")]
        public void RegistrationSuccessful(HttpClient client, 
            RegisterUser input,
            HttpResponseMessage registrationRsp,
            UserRecord userRecord)
        {
            $"Given I have an instance of httpclient"
                .x(() => client = _server.Http());
            $"And my registration details are as below"
                .x(() => input = new RegisterUser
                {
                    Email = $"{Guid.NewGuid().ToString()}@test.com",
                    Password = "pass"
                });
            $"When I submit my details for registration"
                .x(async () => registrationRsp = await client.PostFormDataAsync("/accounts/registration", input));
            $"Then I should get a redirect response"
                .x(() => { registrationRsp.StatusCode.ShouldBe(HttpStatusCode.Redirect); });
            $"And I should be redirected to home page"
                .x(() => { registrationRsp.Headers.Location.ToString().ShouldBe("/"); });
            $"And my details should be stored in db"
                .x(async () =>
                {
                    userRecord = await _server.Services.GetRecordByEmail(input.Email);
                    userRecord.ShouldNotBeNull();
                });
            $"And my password should be stored as hash"
                .x(() => userRecord.PasswordHash.ShouldNotBe(input.Password));
            $"And an email should be sent to my email address"
                .x(() => _server.Services.IsEmailSent(new SendEmailInput
                {
                    TemplateName = "UserRegistration",
                    Subject = "Welcome to BDDshop",
                    To = input.Email
                }).ShouldBeTrue());
            $"And I should be authenticated"
                .x(() => _server.Services.IsAuthenticatedWithEmail(input.Email));
        }
        
        [Trait("Category", TestCategoryNames.Fast)]
        [Scenario(DisplayName = "Registration Failed On Wrong Input")]
        public void RegistrationFailedOnWrongInput(HttpClient http, 
            HttpResponseMessage rsp, 
            ErrorContainer errors)
        {
            $"Given I have an instance of HttpClient"
                .x(() => http = _server.Http());
            $"When I submit an empty form"
                .x(async () => rsp = await http.PostFormDataAsync("/accounts/registration", new RegisterUser()));
            $"Then I should get a response with status code bad request"
                .x(() => rsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest));
            $"And I should get collection of errors"
                .x(async () => errors = await rsp.ReadAsAsync<ErrorContainer>());
            $"And errors should contain error message for empty email"
                .x(() => errors.HasError("Email", "Email is required"));
            $"And errors should contain error message for empty password"
                .x(() => errors.HasError("Password", "Password is required"));
        }
    }
}
