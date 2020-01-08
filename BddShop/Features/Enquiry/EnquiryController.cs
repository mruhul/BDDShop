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

        public EnquiryController(ILeadApiProxy proxy)
        {
            _proxy = proxy;
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(SendEnquiryRequest request)
        {
            var id = new Guid().ToString();

            await _proxy.SendAsync(new LeadProxyInput
            {
                NetworkId = request.NetworkId,
                Id = id
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
