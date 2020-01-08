using System;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Enquiry
{
    [Route("enquiry")]
    public class EnquiryController : Controller
    {
        private readonly ILeadApiProxy _proxy;
        private readonly IProductApiProxy _productApiProxy;
        private readonly IEmailSender _emailSender;

        public EnquiryController(ILeadApiProxy proxy,IProductApiProxy productApiProxy, IEmailSender emailSender)
        {
            _proxy = proxy;
            _productApiProxy = productApiProxy;
            _emailSender = emailSender;
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(SendEnquiryRequest request)
        {
            var stock = await _productApiProxy.GetAsync(request.NetworkId);

            var id = new Guid().ToString();

            await _proxy.SendAsync(new LeadProxyInput
            {
                NetworkId = request.NetworkId,
                Id = id
            });

            await _emailSender.SendAsync(new SendEmailInput
            {
                Subject = "A enquiry posted on your item",
                TemplateName = "EmailSellerLead",
                To = stock.SellerEmail
            });

            await _emailSender.SendAsync(new SendEmailInput
            {
                TemplateName = "EnquiryThankYou",
                Subject = "Thanks for your enquiry",
                To = request.Email
            });

            return Ok(new SendEnquiryResponse
            {
                LeadId = id
            });
        }
    }

    public class SendEnquiryRequest
    {
        public string Email { get; set; }
        public string NetworkId { get; set; }
    }

    public class SendEnquiryResponse
    {
        public string LeadId { get; set; }
    }
}
