using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.RequestBus;

namespace BddShop.Features.ProductDetails
{
    public class LoadProductDetailsHandler : RequestHandlerAsync<LoadProductDetails, ProductDetailsViewModel>
    {
        private readonly IProductApiProxy _productApiProxy;
        private readonly DiscountCalculator _discountCalculator;

        public LoadProductDetailsHandler(IProductApiProxy proxy, 
            DiscountCalculator discountCalculator)
        {
            _productApiProxy = proxy;
            _discountCalculator = discountCalculator;
        }

        protected override async Task<ProductDetailsViewModel> Handle(IExecutionContextReader context, LoadProductDetails request)
        {
            var product = await _productApiProxy.GetAsync(request.Id);

            if (product == null) return null;

            var discount = _discountCalculator.Calculate(product.Price);

            return new ProductDetailsViewModel
            {
                Title = product.Title,
                PriceText = product.Price.ToString("C"),
                DiscountText = discount > 0 ? discount.ToString("C") : string.Empty
            };
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
