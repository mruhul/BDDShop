using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace BddShop.Tests.Infra
{
    public static class ActionResultExtensions
    {
        public static T ViewModel<T>(this IActionResult source)
        {
            if (source == null) return default(T);
            if (source is ViewResult vr)
            {
                return (T)vr.Model;
            }

            return default(T);
        }
    }
}
