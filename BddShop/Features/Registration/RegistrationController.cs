using System.Threading.Tasks;
using Bolt.RequestBus;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Registration
{
    public class RegistrationController : Controller
    {
        private readonly IRequestBus _bus;

        public RegistrationController(IRequestBus bus)
        {
            _bus = bus;
        }

        [HttpGet("accounts/registration")]
        public async Task<IActionResult> Get()
        {
            return View("~/Features/Registration/Views/Index.cshtml", new RegisterUser());
        }

        [HttpPost("accounts/registration")]
        public async Task<IActionResult> Post(RegisterUser input)
        {
            await _bus.SendAsync(input);

            return Redirect("/");
        }
    }
}
