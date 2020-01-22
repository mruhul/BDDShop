using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.IocAttributes;

namespace BddShop.Features.Enquiry
{
    [AutoBind]
    public class EnquiryEmailSender
    {
        private readonly IEmailSender _emailSender;

        public EnquiryEmailSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public ValueTask SendAsync(string sellerEmail)
        {
            return _emailSender.SendAsync(new SendEmailInput
            {
                To = sellerEmail,
                TemplateName = "lead-email",
                Subject = "someone enquired for your product",
                Data = new object() // dummy for now :)
            });
        }
    }
}