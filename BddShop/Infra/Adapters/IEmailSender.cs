using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BddShop.Infra.Adapters
{
    public interface IEmailSender
    {
        ValueTask SendAsync(SendEmailInput input);
    }

    public class SendEmailInput
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string TemplateName { get; set; }
        public object Data { get; set; }
    }
}
