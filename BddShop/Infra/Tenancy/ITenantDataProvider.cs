using System.Collections.Generic;

namespace BddShop.Infra.Tenancy
{
    public interface ITenantDataProvider<T>
    {
        Dictionary<string,T> Get();
    }
}