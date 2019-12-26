using System.Threading.Tasks;
using BddShop.Infra;
using Bolt.RequestBus;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Login
{
    public class LoginController : Controller
    {
        private readonly IRequestBus _bus;

        public LoginController(IRequestBus bus)
        {
            _bus = bus;
        }

        [Validate]
        [HttpPost("accounts/login")]
        public async Task<IActionResult> Post(LoginRequest request)
        {
            var rsp = await _bus.SendAsync(request);

            return rsp.ToRedirectResponse("/");
        }
    }
}
