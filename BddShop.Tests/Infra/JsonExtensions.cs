using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Shouldly;

namespace BddShop.Tests.Infra
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T source) => JsonConvert.SerializeObject(source);

        public static void ShouldBeSameContent<TInput, TExpected>(this TInput source, TExpected expected) =>
            source.ToJson().ShouldBe(expected.ToJson());
    }
}
