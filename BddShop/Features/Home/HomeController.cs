using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.Home
{
    public class HomeController : Controller
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
