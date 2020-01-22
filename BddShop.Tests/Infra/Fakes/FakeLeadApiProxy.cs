using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Infra.Fakes
{
    public class FakeLeadApiProxy : ILeadApiProxy
    {
        private Dictionary<string,LeadProxyInput> _data = new Dictionary<string, LeadProxyInput>();

        public ValueTask SendAsync(LeadProxyInput input)
        {
            _data[input.Id] = input;

            return new ValueTask();
        }

        internal LeadProxyInput GetInputById(string id) => _data.TryGetValue(id, out var result) ? result : null;
    }

    public static class FakeLeadApiProxyExtensions
    {
        public static LeadProxyInput GetProxyInputSent(this IServiceProvider source, string id)
        {
            return (source.GetService<ILeadApiProxy>() as FakeLeadApiProxy).GetInputById(id);
        }
        public static LeadProxyInput GetProxyInputSent(this IServiceScope source, string id)
        {
            return source.GetServiceOfType<ILeadApiProxy,FakeLeadApiProxy>().GetInputById(id);
        }
    }
}
