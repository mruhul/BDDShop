using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BddShop.Tests.Infra
{
    public static class HttpClientHelper
    {
        public static Task PostFormDataAsync(this HttpClient source, string url, Dictionary<string, string> data)
        {
            return source.PostAsync(url, new FormUrlEncodedContent(data));
        }
        public static Task<HttpResponseMessage> PostFormDataAsync<T>(this HttpClient source, string url, T input)
        {
            var data = new Dictionary<string,string>();

            var props = input.GetType().GetProperties().Where(x => x.CanRead);

            foreach (var prop in props)
            {
                data[prop.Name] = prop.GetValue(input)?.ToString();
            }

            return source.PostAsync(url, new FormUrlEncodedContent(data));
        }
    }
}
