using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt.Common.Extensions;

namespace BddShop.Infra.Adapters.Impl
{
    internal sealed class RealProductApiProxy : IProductApiProxy
    {
        private readonly List<ProductRecord> _data = new List<ProductRecord>
        {
            new ProductRecord
            {
                Id = "PRD-0001",
                Price = 20,
                Title = "BDD TShirt"
            },
            new ProductRecord
            {
                Id = "PRD-0001",
                Price = 10,
                Title = "BDD Mug"
            }
        };

        public ValueTask<ProductRecord> GetAsync(string id)
        {
            return new ValueTask<ProductRecord>(_data.FirstOrDefault(x => x.Id.IsSame(id)));
        }
    }
}
