using System;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.IocAttributes;

namespace BddShop.Features.Enquiry
{
    [AutoBind]
    public class LeadService
    {
        private readonly ILeadApiProxy _leadApiProxy;

        public LeadService(ILeadApiProxy leadApiProxy)
        {
            _leadApiProxy = leadApiProxy;
        }

        public async ValueTask<string> SendLead(SendEnquiryInput request, ProductRecord product)
        {
            var id = Guid.NewGuid().ToString();

            await _leadApiProxy.SendAsync(new LeadProxyInput
            {
                NetworkId = request.NetworkId,
                Id = id,
                Email = request.Email,
                Price = product.Price
            });

            return id;
        }
    }
}