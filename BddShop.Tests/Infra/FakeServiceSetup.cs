using BddShop.Infra.Adapters;
using BddShop.Tests.Infra.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BddShop.Tests.Infra
{
    public static class FakeServiceSetup
    {
        public static void Run(IServiceCollection sc, bool isScoped)
        {
            if (isScoped)
            {
                sc.Replace(ServiceDescriptor.Scoped<IUserStore, FakeUserStore>());
                sc.Replace(ServiceDescriptor.Scoped<IEmailSender, FakeEmailSender>());
                sc.Replace(ServiceDescriptor.Scoped<IAuthenticator, FakeAuthenticator>());
                sc.Replace(ServiceDescriptor.Scoped<IProductApiProxy, FakeProductApiProxy>());
                sc.Replace(ServiceDescriptor.Scoped<ITenant, FakeTenant>());
            }
            else
            {
                sc.Replace(ServiceDescriptor.Singleton<IUserStore, FakeUserStore>());
                sc.Replace(ServiceDescriptor.Singleton<IEmailSender, FakeEmailSender>());
                sc.Replace(ServiceDescriptor.Singleton<IAuthenticator, FakeAuthenticator>());
                sc.Replace(ServiceDescriptor.Singleton<IProductApiProxy, FakeProductApiProxy>());
            }
        }
    }
}