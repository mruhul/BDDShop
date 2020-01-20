using Bolt.RequestBus;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Infra
{
    public static class ServiceScopeExtensions
    {
        public static T GetService<T>(this IServiceScope source) => source.ServiceProvider.GetService<T>();
        public static IRequestBus RequestBus(this IServiceScope source) => source.GetService<IRequestBus>();
    }
}