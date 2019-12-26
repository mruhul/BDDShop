using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bolt.RequestBus;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Infra
{
    public static class RequestBusExtensions
    {
        public static IActionResult ToRedirectResponse(this IResponse rsp, string redirectTo, bool isPermanent = false)
        {
            if(rsp.IsSucceed) return new RedirectResult(redirectTo, isPermanent);

            var errors = new Dictionary<string,ICollection<string>>();

            foreach (var error in rsp.Errors)
            {
                if (!errors.ContainsKey(error.PropertyName))
                {
                    errors[error.PropertyName] = new List<string>();
                }

                errors[error.PropertyName].Add(error.Message);
            }

            return new BadRequestObjectResult(new ErrorContainer
            {
                Errors = errors
            });
        }
    }
}
