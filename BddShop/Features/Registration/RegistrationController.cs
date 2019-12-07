using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using BddShop.Infra.Crypto;
using Bolt.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Registration
{
    public class RegistrationController : Controller
    {
        private readonly IUserStore _userStore;
        private readonly ICrypto _crypto;

        public RegistrationController(IUserStore userStore, ICrypto crypto)
        {
            _userStore = userStore;
            _crypto = crypto;
        }

        [HttpPost("accounts/registration")]
        public async Task<IActionResult> Post(RegisterUser input)
        {
            await _userStore.Create(new UserRecord
            {
                Id = Guid.NewGuid().ToString(),
                Email = input.Email,
                PasswordHash = _crypto.Hash(input.Password, input.Email)
            });

            return Ok();
        }
    }
}
