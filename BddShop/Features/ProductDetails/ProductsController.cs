using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.RequestBus;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.ProductDetails
{
    public class ProductsController : Controller
    {
        private readonly IRequestBus _bus;

        public ProductsController(IRequestBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IActionResult> Get(LoadProductDetails input)
        {
            var rsp = await _bus.SendAsync<LoadProductDetails, ProductDetailsViewModel>(input);

            if (rsp.Result == null) return NotFound();

            return View("~/Features/ProductsDetails/Views/Index.cshtml", rsp.Result);
        }
    }

}
