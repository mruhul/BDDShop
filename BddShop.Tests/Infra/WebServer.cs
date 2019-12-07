using BddShop.Infra.Adapters;
using BddShop.Tests.Infra.Fakes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            sc.Replace(ServiceDescriptor.Singleton<IUserStore, FakeUserStore>());
        }
    }
    
    [CollectionDefinition(TestCollectionNames.WebServer)]
    public class WebServerXUnitCollection : ICollectionFixture<WebServer>
    {}

    public static class TestCollectionNames
    {
        public const string WebServer = "WebServer";
    }
}
