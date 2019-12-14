using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.Common.Extensions;
using Bolt.RequestBus;

namespace BddShop.Features.Home.Showroom
{
    public class ShowroomRequestHandler : RequestHandlerAsync<ShowroomRequest,ShowroomViewModel>
    {
        private readonly ITenant _tenant;
        private readonly IEnumerable<IShowroomViewModelProvider> _providers;

        public ShowroomRequestHandler(ITenant tenant, IEnumerable<IShowroomViewModelProvider> providers)
        {
            _tenant = tenant;
            _providers = providers;
        }

        protected override Task<ShowroomViewModel> Handle(IExecutionContextReader context, ShowroomRequest request)
        {
            var tenantName = _tenant.Name;
            var provider = _providers.FirstOrDefault(x => x.ForTenants.Any(tenant => tenantName.IsSame(tenant)));
            return provider == null 
                    ? Task.FromResult<ShowroomViewModel>(null) 
                    : Task.FromResult(provider.Get());
        }
    }

    public class ShowroomRequest
    {

    }

    public class ShowroomViewModel
    {
        public string Heading { get; set; }
        public HtmlLink ViewAllLink { get; set; }
    }

    public class HtmlLink
    {
        public string Text { get; set; }
        public string Url { get; set; }
    }
}
