using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BddShop.Tests.Infra
{
    public class WebServer : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(SetupFakes);
        }

        private void SetupFakes(IServiceCollection sc)
        {
            FakeServiceSetup.Run(sc, false);
        }

        public HttpClient Http(bool allowAutoRedirect = false)
        {
            return this.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = allowAutoRedirect
            });
        }
    }
    
    [CollectionDefinition(TestCollectionNames.WebServer)]
    public class WebServerXUnitCollection : ICollectionFixture<WebServer>
    {}

    public static class TestCollectionNames
    {
        public const string WebServer = "WebServer";
        public const string IocFixture = "IocFixture";
    }

    public static class TestCategoryNames
    {
        public const string Slow = "Slow";
        public const string Fast = "Fast";
    }
}
