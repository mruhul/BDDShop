using System;
using System.Security.Cryptography;
using System.Text;
using Bolt.IocAttributes;

namespace BddShop.Infra.Crypto
{
    [AutoBind(LifeCycle.Singleton)]
    internal sealed  class Crypto : ICrypto
    {
        public string Hash(string value, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(value + salt);
            var algorithm = new SHA256Managed();
            var hash = algorithm.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public interface ICrypto
    {
        string Hash(string value, string salt);
    }
}
