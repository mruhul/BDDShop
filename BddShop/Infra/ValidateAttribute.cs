using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BddShop.Infra
{
    public class ValidateAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errCnt = new ErrorContainer();

                foreach (var item in context.ModelState)
                {
                    errCnt.AddError(item.Key, item.Value.Errors.Select(x => x.ErrorMessage).ToArray());
                }

                context.Result = new BadRequestObjectResult(errCnt);
                return;
            }

            await next();
        }
    }

    public class ErrorContainer
    {
        public Dictionary<string,ICollection<string>> Errors { get; set; }
            = new Dictionary<string, ICollection<string>>();

        public void AddError(string name, params string[] msg)
        {
            Errors.TryGetValue(name, out var err);

            if (err == null)
            {
                err = new List<string>();

                Errors[name] = err;
            }

            foreach (var errMsg in msg)
            {
                err.Add(errMsg);
            }
        }
    }
}
