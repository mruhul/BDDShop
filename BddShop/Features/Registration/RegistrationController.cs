using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Registration
{
    public class RegistrationController : Controller
    {
        private readonly IUserStore _userStore;

        public RegistrationController(IUserStore userStore)
        {
            _userStore = userStore;
        }

        [HttpPost("accounts/registration")]
        public async Task<IActionResult> Post(RegisterUser input)
        {
            await _userStore.Create(new UserRecord
            {
                Id = Guid.NewGuid().ToString(),
                Email = input.Email,
                PasswordHash = input.Password
            });

            return Ok();
        }
    }
}
