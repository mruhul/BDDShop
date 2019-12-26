using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using BddShop.Infra.Crypto;
using Bolt.IocAttributes;

namespace BddShop.Features.Login
{
    [AutoBind]
    internal sealed class UserService
    {
        private readonly IUserStore _userStore;
        private readonly ICrypto _crypto;

        public UserService(IUserStore userStore, ICrypto crypto)
        {
            _userStore = userStore;
            _crypto = crypto;
        }

        public async ValueTask<UserRecord> Retrieve(LoginRequest request)
        {
            var user = await _userStore.GetByEmail(request.Email);

            if (user == null) return null;

            return user.PasswordHash == _crypto.Hash(request.Password, request.Email) 
                ? user 
                : null;
        }
    }
}
