using System.Threading.Tasks;

namespace BddShop.Infra.Adapters.Impl
{
    internal sealed  class EmailSender : IEmailSender
    {
        public ValueTask SendAsync(SendEmailInput input)
        {
            // assume we sending :) Ignore for now
            return new ValueTask();
        }
    }
}
