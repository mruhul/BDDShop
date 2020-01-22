using System;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.Common.Extensions;
using Bolt.IocAttributes;
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

            var response  = await _leadService.SendLead(request, product);

            await _emailSender.SendAsync(product.SellerEmail);

            return new SendEnquiryResponse{ LeadId = response};
        }
    }

    [AutoBind]
    public class EnquiryEmailSender
    {
        private readonly IEmailSender _emailSender;

        public EnquiryEmailSender(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public ValueTask SendAsync(string sellerEmail)
        {
            return _emailSender.SendAsync(new SendEmailInput
            {
                To = sellerEmail,
                TemplateName = "lead-email",
                Subject = "someone enquired for your product",
                Data = new object() // dummy for now :)
            });
        }
    }
        
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