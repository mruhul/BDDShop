using System;
using System.Net;
using System.Net.Http;
using BddShop.Features.Login;
using BddShop.Infra;
using BddShop.Infra.Adapters;
using BddShop.Infra.Crypto;
using BddShop.Tests.Infra;
using BddShop.Tests.Infra.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xbehave;
using Xunit;

namespace BddShop.Tests.Features.Login
{
    [Collection(TestCollectionNames.WebServer)]
    public class LoginFeatureTests
    {
        private readonly WebServer _server;

        public LoginFeatureTests(WebServer server)
        {
            _server = server;
        }

        [Scenario(DisplayName = "ShouldAbleToLogin")]
        public void ShouldAbleToLogin(HttpClient client, HttpResponseMessage loginRsp)
        {
            var id = Guid.NewGuid().ToString();
            var email = $"{id}@gmail.com";
            var password = "test";
            var passwordHash = _server.Services.GetService<ICrypto>().Hash(password, email);

            $"Given I have an instance of httpclient"
                .x(() => client = _server.Http());
            
            $"Given I am registered with username and password"
                .x(() => _server.Services.EnsureUserRecordExists(new UserRecord
                {
                    Id = id,
                    Email = email,
                    PasswordHash = passwordHash
                }));
            
            $"When I submit my username and password"
                .x(async () => loginRsp = await client.PostFormDataAsync($"/accounts/login/",
                    new LoginRequest
                    {
                        Email = email, 
                        Password = password
                    }));

            $"And I should be authenticated"
                .x(() => _server.Services.IsAuthenticatedWithEmail(email).ShouldBeTrue());
            
            $"Then I should get a redirect response"
                .x(() => { loginRsp.StatusCode.ShouldBe(HttpStatusCode.Redirect); });
            
            $"And I should be redirected to home page"
                .x(() => { loginRsp.Headers.Location.ToString().ShouldBe("/"); });
        }

        [Scenario(DisplayName = "LoginFailure")]
        [InlineData("notexistingemail@test.com","pass", true )]
        [InlineData("existing-email@test.com","wrongpass", false)]
        public void LoginFailure(string email, string password, bool existingEmail, HttpClient client, HttpResponseMessage loginRsp, ErrorContainer errors)
        {
            $"Given I have an instance of httpclient"
                .x(() => client = _server.Http());

            if (existingEmail)
            {
                $"And my email should exists in system"
                    .x(() => _server.Services.EnsureUserRecordExists(new UserRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        PasswordHash = Guid.NewGuid().ToString()
                    }));
            }

            $"When I submit my login details"
                .x(async () => loginRsp = await client.PostFormDataAsync("/accounts/login/", new LoginRequest
                {
                    Email = email,
                    Password = password
                }));
            $"Then I should receive a bad request status code"
                .x(() => loginRsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest));
            $"And the response should contain error collection"
                .x(async () => errors = await loginRsp.ReadAsAsync<ErrorContainer>());
            $"And error should contain correct error message"
                .x(() => errors.HasError("Email",
                    "Failed to login. Please ensure you entered correct email and password."));
        }

        [Scenario(DisplayName = "Login Validation Failed")]
        public void LoginValidation(HttpClient client, HttpResponseMessage loginRsp, ErrorContainer errors)
        {
            $"Given I have an instance of httpclient"
                .x(() => client = _server.Http());
            $"When I sunmit an empty login form"
                .x(async () => loginRsp = await client.PostFormDataAsync("/accounts/login/", new LoginRequest()));
            $"Then I should get a badrequest status code"
                .x(() => loginRsp.StatusCode.ShouldBe(HttpStatusCode.BadRequest));
            $"And response should have an error container"
                .x(async () => errors = await loginRsp.ReadAsAsync<ErrorContainer>());
            $"And error should contain 2 errors"
                .x(() => errors.Errors.Count.ShouldBe(2));
            $"And error should contain error for empty email"
                .x(() => errors.HasError("Email", "Email is required."));
            $"And error should contain error for empty password"
                .x(() => errors.HasError("Password", "Password is required."));
        }
    }
}
