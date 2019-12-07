using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.IocAttributes;

namespace BddShop.Features.Registration
{
    [AutoBind]
    public class RegistrationEmailSender
    {
        private readonly IEmailSender _emailSender;

        public RegistrationEmailSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async ValueTask SendAsync(RegisterUser request)
        {
            await _emailSender.SendAsync(new SendEmailInput
            {
                TemplateName = "UserRegistration",
                Subject = "Welcome to BDDshop",
                To = request.Email
            });
        }
    }
}
