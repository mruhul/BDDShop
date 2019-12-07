using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Bolt.Common.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Infra.Fakes
{
    public class FakeUserStore : IUserStore
    {
        private readonly ConcurrentDictionary<string,UserRecord> _store = new ConcurrentDictionary<string, UserRecord>();

        public ValueTask Create(UserRecord record)
        {
            _store.TryAdd(record.Id, record);
            return new ValueTask();
        }

        internal UserRecord GetByEmail(string email) => _store.Values.FirstOrDefault(x => x.Email.IsSame(email));
    }

    public static class FakeUserStoreExtensions
    {
        public static UserRecord GetRecordByEmail(this IServiceProvider source, string email)
        {
            return ((FakeUserStore) source.GetService<IUserStore>()).GetByEmail(email);
        }
    }
}
