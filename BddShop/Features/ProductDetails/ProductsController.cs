using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Features.ProductDetails
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public IActionResult Get(LoadProductDetails input)
        {
            return View("~/Features/ProductsDetails/Views/Index.cshtml",
                new ProductDetailsViewModel());
        }
    }

    public class ProductDetailsViewModel
    {
        
    }

    public class LoadProductDetails
    {
        public string Id { get; set; }
    }
}
