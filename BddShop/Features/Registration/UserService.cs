using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using BddShop.Infra.Crypto;
using Bolt.IocAttributes;

namespace BddShop.Features.Registration
{
    [AutoBind]
    public class UserService
    {
        private readonly IUserStore _userStore;
        private readonly ICrypto _crypto;

        public UserService(IUserStore userStore, ICrypto crypto)
        {
            _userStore = userStore;
            _crypto = crypto;
        }

        public async ValueTask<string> Create(RegisterUser request)
        {
            var id = Guid.NewGuid().ToString();

            await _userStore.Create(new UserRecord
            {
                Id = id,
                Email = request.Email,
                PasswordHash = _crypto.Hash(request.Password, request.Email)
            });

            return id;
        }
    }
}
