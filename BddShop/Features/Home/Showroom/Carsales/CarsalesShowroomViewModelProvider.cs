using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BddShop.Features.Shared;

namespace BddShop.Features.Home.Showroom.Carsales
{
    public class CarsalesShowroomViewModelProvider : IShowroomViewModelProvider
    {
        public string[] ForTenants => new[] {TenantNames.Carsales};
        public ShowroomViewModel Get()
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
    }
}
