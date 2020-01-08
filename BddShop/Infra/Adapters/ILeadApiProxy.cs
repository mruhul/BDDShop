using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BddShop.Infra.Adapters
{
    public interface ILeadApiProxy
    {
        ValueTask SendAsync(LeadProxyInput input);
    }

    public class LeadProxyInput
    {
        public string Id { get; set; }
        public string NetworkId { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
    }
}
