﻿using Microsoft.AspNetCore.Hosting;
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