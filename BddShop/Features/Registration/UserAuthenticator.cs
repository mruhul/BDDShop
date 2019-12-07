using System.Security.Claims;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.IocAttributes;

namespace BddShop.Features.Registration
{
    [AutoBind]
    internal sealed  class UserAuthenticator
    {
        private readonly IAuthenticator _authenticator;

        public UserAuthenticator(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public async ValueTask Authenticate(UserAuthRequest request)
        {
            await _authenticator.Authenticate(new[]
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Name, request.Id)
            });
        }
    }

    public class UserAuthRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}