using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Infra.Fakes
{
    public class FakeAuthenticator : IAuthenticator
    {
        private readonly ConcurrentDictionary<string,IEnumerable<Claim>> _store = new ConcurrentDictionary<string, IEnumerable<Claim>>();

        public ValueTask Authenticate(IEnumerable<Claim> claims)
        {
            _store.TryAdd(claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value, claims);
            return new ValueTask();
        }

        public bool IsClaimForEmailExists(string email) => _store.ContainsKey(email);
    }

    public static class FakeAuthenticatorExtensions
    {
        public static bool IsAuthenticatedWithEmail(this IServiceProvider source, string email)
        {
            return ((FakeAuthenticator) source.GetService<IAuthenticator>()).IsClaimForEmailExists(email);
        }
    }
}
