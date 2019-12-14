using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt.RequestBus;

namespace BddShop.Features.Home.Showroom
{
    public class ShowroomRequestHandler : RequestHandlerAsync<ShowroomRequest,ShowroomViewModel>
    {
        protected override Task<ShowroomViewModel> Handle(IExecutionContextReader context, ShowroomRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class ShowroomRequest
    {

    }

    public class ShowroomViewModel
    {

    }
}
