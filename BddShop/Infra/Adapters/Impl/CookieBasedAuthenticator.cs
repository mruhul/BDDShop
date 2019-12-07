using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace BddShop.Infra.Adapters.Impl
{
    public class CookieBasedAuthenticator : IAuthenticator
    {
        private readonly IHttpContextAccessor _context;

        public CookieBasedAuthenticator(IHttpContextAccessor context)
        {
            _context = context;
        }

        public ValueTask Authenticate(IEnumerable<Claim> claims)
        {
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            _context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());
            return new ValueTask();
        }
    }
}
