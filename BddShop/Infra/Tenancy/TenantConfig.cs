using System;
using System.Collections.Generic;

namespace BddShop.Infra.Tenancy
{
    public interface ITenantConfig<out T>
    {
        T Current { get; }
        string TenantName { get; }
    }

    internal sealed class TenantConfig<T> : ITenantConfig<T>
    {
        private readonly ITenant _tenant;
        private readonly IEnumerable<ITenantDataProvider<T>> _providers;

        private static volatile Dictionary<string, T> _data;
        private static volatile object _lock = new object();

        public TenantConfig(ITenant tenant, IEnumerable<ITenantDataProvider<T>> providers)
        {
            _tenant = tenant;
            _providers = providers;
        }

        public T Current =>
            Data.TryGetValue(_tenant.Name, out var config) 
                ? config 
                : default;

        public string TenantName => _tenant.Name;

        private Dictionary<string, T> Data
        {
            get
            {
                if (_data != null) return _data;

                lock (_lock)
                {
                    if (_data != null) return _data;

                    _data = BuildData();
                }

                return _data;
            }
        }

        private Dictionary<string, T> BuildData()
        {
            var result = new Dictionary<string,T>();

            foreach (var provider in _providers)
            {
                var data = provider.Get();

                foreach (var item in data)
                {
                    result[item.Key] = item.Value;
                }
            }

            return result;
        }
    }
}