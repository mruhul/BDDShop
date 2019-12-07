using System;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using BddShop.Infra.Crypto;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Registration
{
    public class RegistrationController : Controller
    {
        private readonly IUserStore _userStore;
        private readonly ICrypto _crypto;
        private readonly IEmailSender _emailSender;

        public RegistrationController(IUserStore userStore, ICrypto crypto, IEmailSender emailSender)
        {
            _userStore = userStore;
            _crypto = crypto;
            _emailSender = emailSender;
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

            await _emailSender.SendAsync(new SendEmailInput
            {
                TemplateName = "UserRegistration",
                Subject = "Welcome to BDDshop",
                To = input.Email
            });

            return Ok();
        }
    }
}
