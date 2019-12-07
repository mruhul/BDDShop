using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Registration
{
    public class RegistrationController : Controller
    {
        [HttpPost("accounts/registration")]
        public async Task<IActionResult> Post(RegisterUser input)
        {
            return Ok();
        }
    }
}
