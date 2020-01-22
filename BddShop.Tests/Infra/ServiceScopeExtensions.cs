using System.Linq;
using Bolt.RequestBus;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Infra
{
    public static class ServiceScopeExtensions
    {
        public static T GetService<T>(this IServiceScope source) => source.ServiceProvider.GetRequiredService<T>();
        public static IRequestBus RequestBus(this IServiceScope source) => source.GetService<IRequestBus>();

        public static TImpl GetServiceOfType<TInterface, TImpl>(this IServiceScope scope) where TImpl : TInterface =>
            (TImpl) scope.ServiceProvider.GetRequiredService<TInterface>();
        public static TImpl FindServiceOfType<TInterface, TImpl>(this IServiceScope scope) where TImpl : TInterface =>
            (TImpl) scope.ServiceProvider.GetServices<TInterface>().FirstOrDefault(x => x is TImpl);
    }
}