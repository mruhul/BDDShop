namespace BddShop.Infra.Tenancy
{
    internal sealed class Tenant : ITenant
    {
        private string _tenant = string.Empty;

        public string Name => _tenant;

        internal void SetTenant(string tenant)
        {
            _tenant = tenant;
        }
    }
}
