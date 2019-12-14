using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BddShop.Features.Shared;

namespace BddShop.Features.Home.Showroom.Bikesales
{
    public class BikesalesShowroomViewModelProvider : IShowroomViewModelProvider
    {
        public string[] ForTenants => new[] {TenantNames.Bikesales};
        public ShowroomViewModel Get()
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
    }
}
