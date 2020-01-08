using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Enquiry
{
    [Route("enquiry")]
    public class EnquiryController : Controller
    {
        [HttpPost("")]
        public async Task<ActionResult> Post(SendEnquiryRequest request)
        {
            return Ok(new SendEnquiryResponse
            {
                LeadId = Guid.NewGuid().ToString()
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
