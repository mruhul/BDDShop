using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BddShop.Features.Shared;
using BddShop.Infra.Adapters;
using Bolt.Common.Extensions;
using Bolt.RequestBus;

namespace BddShop.Features.Home.Showroom
{
    public class ShowroomRequestHandler : RequestHandlerAsync<ShowroomRequest,ShowroomViewModel>
    {
        private readonly ITenant _tenant;

        public ShowroomRequestHandler(ITenant tenant)
        {
            _tenant = tenant;
        }

        protected override async Task<ShowroomViewModel> Handle(IExecutionContextReader context, ShowroomRequest request)
        {
            if (_tenant.Name.IsSame(TenantNames.Carsales))
            {
                return new ShowroomViewModel
                {
                    Heading = "New Car Showroom",
                    ViewAllLink = new HtmlLink
                    {
                        Url = "https://www.carsales.com.au/new-cars/#start-search",
                        Text = "View all body types"
                    }
                };
            }
            else if (_tenant.Name.IsSame(TenantNames.Bikesales))
            {
                return new ShowroomViewModel
                {
                    Heading = "New Bike Showroom",
                    ViewAllLink = new HtmlLink
                    {
                        Url = "https://www.bikesales.com.au/new-bikes/#start-search",
                        Text = "View all body types"
                    }
                };
            }
            else
            {
                return null;
            }
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
