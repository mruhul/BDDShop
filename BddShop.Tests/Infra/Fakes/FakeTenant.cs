using System;
using BddShop.Infra.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Infra.Fakes
{
    public class FakeTenant : ITenant
    {
        public string Name { get; internal set; }

        internal void SetTenant(string name) => Name = name;
    }

    public static class FakeTenantExtensions
    {
        public static void SetCurrentTenant(this IServiceProvider sp, string tenant)
        {
            ((FakeTenant)sp.GetRequiredService<ITenant>()).SetTenant(tenant);
        }
    }
}
