using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BddShop.Infra.Adapters
{
    public interface IAuthenticator
    {
        ValueTask Authenticate(IEnumerable<Claim> claims);
    }
}
