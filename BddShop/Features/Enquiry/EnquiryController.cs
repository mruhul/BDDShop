using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Enquiry
{
    [Route("enquiry")]
    public class EnquiryController : Controller
    {
        [HttpPost("")]
        public ActionResult Post()
        {
            return Ok();
        }
    }
}
