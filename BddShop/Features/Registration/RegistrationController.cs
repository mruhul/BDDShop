using System;
using System.Security.Claims;
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
        private readonly IAuthenticator _authenticator;

        public RegistrationController(IUserStore userStore, 
            ICrypto crypto, 
            IEmailSender emailSender, 
            IAuthenticator authenticator)
        {
            _userStore = userStore;
            _crypto = crypto;
            _emailSender = emailSender;
            _authenticator = authenticator;
        }

        [HttpPost("accounts/registration")]
        public async Task<IActionResult> Post(RegisterUser input)
        {
            var id = Guid.NewGuid().ToString();

            await _userStore.Create(new UserRecord
            {
                Id = id,
                Email = input.Email,
                PasswordHash = _crypto.Hash(input.Password, input.Email)
            });

            await _emailSender.SendAsync(new SendEmailInput
            {
                TemplateName = "UserRegistration",
                Subject = "Welcome to BDDshop",
                To = input.Email
            });

            await _authenticator.Authenticate(new[]
            {
                new Claim(ClaimTypes.Email, input.Email),
                new Claim(ClaimTypes.Name, id)
            });

            return Redirect("/");
        }
    }
}
