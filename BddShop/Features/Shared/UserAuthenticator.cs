using System.Security.Claims;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.IocAttributes;

namespace BddShop.Features.Shared
{
    public interface IUserAuthenticator
    {
        ValueTask Authenticate(UserAuthRequest request);
    }

    [AutoBind]
    internal sealed  class UserAuthenticator : IUserAuthenticator
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
                new Claim(ClaimTypes.NameIdentifier, request.Id)
            });
        }
    }

    public class UserAuthRequest
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}