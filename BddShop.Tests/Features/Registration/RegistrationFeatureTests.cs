using System;
using System.Net.Http;
using BddShop.Tests.Infra;
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
        public void RegistrationSuccessful(HttpClient client)
        {
            $"Given I have an instance of httpclient"
                .x(() => client = _server.CreateClient());
            $"When I submit my details for registration"
                .x(() => throw new NotImplementedException());
            $"Then I should get a okay response"
                .x(() => throw new NotImplementedException());
            $"And my details should be stored"
                .x(() => throw new NotImplementedException());
            $"And my password should be stored as hash"
                .x(() => throw new NotImplementedException());
            $"And and email should be sent to my email address"
                .x(() => throw new NotImplementedException());
            $"And I should be authenticated"
                .x(() => throw new NotImplementedException());
            $"And I should be redirected to home page"
                .x(() => throw new NotImplementedException());
        }
    }
}
