using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BddShop.Infra;
using Bolt.Common.Extensions;
using Newtonsoft.Json;

namespace BddShop.Tests.Infra
{
    public static class ErrorContainerExtensions
    {
        public static bool HasError(this ErrorContainer source, string propertyName)
        {
            return source.Errors.Any(x => x.Key.IsSame(propertyName));
        }

        public static bool HasError(this ErrorContainer source, string propertyName, string msg)
        {
            var error = source.Errors.FirstOrDefault(x => x.Key.IsSame(propertyName));
            return error.Value.Any(x => x.IsSame(msg));
        }
    }

    public static class HttpResponseMsgExtensions
    {
        public static async ValueTask<T> ReadAsAsync<T>(this HttpResponseMessage msg)
        {
            using var cnt = msg.Content;

            var body = await cnt.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}
