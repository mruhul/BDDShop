using System.Threading.Tasks;

namespace BddShop.Infra.Adapters
{
    public interface IProductApiProxy
    {
        ValueTask<ProductRecord> GetAsync(string id);
    }

    public class ProductRecord
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
