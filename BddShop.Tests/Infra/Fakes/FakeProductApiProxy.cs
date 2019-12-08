using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Infra.Fakes
{
    internal sealed class FakeProductApiProxy : IProductApiProxy
    {
        private readonly ConcurrentDictionary<string,ProductRecord> _data = new ConcurrentDictionary<string, ProductRecord>();

        public ValueTask<ProductRecord> GetAsync(string id)
        {
            return new ValueTask<ProductRecord>(_data.GetValueOrDefault(id));
        }

        internal void AddProductRecord(ProductRecord record)
        {
            _data.AddOrUpdate(record.Id, record,(d,t) => record);
        }
    }

    public static class FakeProductApiProxyExtensions
    {
        public static void EnsureProductRecordExists(this IServiceProvider sp, ProductRecord record)
        {
            ((FakeProductApiProxy)sp.GetService<IProductApiProxy>()).AddProductRecord(record);
        }
    }
}
