using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.ProductDetails
{
    public class ProductsController : Controller
    {
        private readonly IProductApiProxy _productApiProxy;

        public ProductsController(IProductApiProxy productApiProxy)
        {
            _productApiProxy = productApiProxy;
        }

        [HttpGet]
        public async Task<IActionResult> Get(LoadProductDetails input)
        {
            var product = await _productApiProxy.GetAsync(input.Id);

            return View("~/Features/ProductsDetails/Views/Index.cshtml",
                new ProductDetailsViewModel
                {
                    Title = product.Title
                });
        }
    }

    public class ProductDetailsViewModel
    {
        public string Title { get; set; }
    }

    public class LoadProductDetails
    {
        public string Id { get; set; }
    }
}
