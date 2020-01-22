using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.RequestBus;

namespace BddShop.Features.Enquiry
{
    public class SendEnquiryRequestHandler : RequestHandlerAsync<SendEnquiryInput,SendEnquiryResponse>
    {
        private readonly LeadService _leadService;
        private readonly IProductApiProxy _productApiProxy;
        private readonly EnquiryEmailSender _emailSender;

        public SendEnquiryRequestHandler(LeadService leadService, 
            IProductApiProxy productApiProxy, 
            EnquiryEmailSender emailSender)
        {
            _leadService = leadService;
            _productApiProxy = productApiProxy;
            _emailSender = emailSender;
        }

        protected override async Task<SendEnquiryResponse> Handle(IExecutionContextReader context, SendEnquiryInput request)
        {
            var product = await _productApiProxy.GetAsync(request.NetworkId);

            var response  = await _leadService.SendLeadAsync(request, product);

            await _emailSender.SendAsync(product.SellerEmail);

            return new SendEnquiryResponse{ LeadId = response};
        }
    }
}