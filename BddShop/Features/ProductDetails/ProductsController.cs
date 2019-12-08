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

            var discount = CalculateDiscount(product.Price);

            return View("~/Features/ProductsDetails/Views/Index.cshtml",
                new ProductDetailsViewModel
                {
                    Title = product.Title,
                    PriceText = product.Price.ToString("C"),
                    DiscountText = discount > 0 ? discount.ToString("C") : string.Empty
                });
        }

        private decimal CalculateDiscount(decimal amount)
        {
            if (amount >= 100) return 5;
            if (amount >= 50) return 2;
            if (amount >= 10) return 1;

            return 0;
        }
    }

    public class ProductDetailsViewModel
    {
        public string Title { get; set; }
        public string PriceText { get; set; }
        public string DiscountText { get; set; }
    }

    public class LoadProductDetails
    {
        public string Id { get; set; }
    }
}
