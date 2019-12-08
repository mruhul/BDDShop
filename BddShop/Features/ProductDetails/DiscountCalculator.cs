using Bolt.IocAttributes;

namespace BddShop.Features.ProductDetails
{
    [AutoBind]
    public sealed class DiscountCalculator
    {
        public decimal Calculate(decimal price)
        {
            if (price >= 100) return 5;
            if (price >= 50) return 2;
            if (price >= 10) return 1;

            return 0;
        }
    }
}