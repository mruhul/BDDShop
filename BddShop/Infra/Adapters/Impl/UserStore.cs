using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace BddShop.Infra.Adapters.Impl
{
    internal sealed  class UserStore : IUserStore
    {
        private static ConcurrentDictionary<string,UserRecord> _store = new ConcurrentDictionary<string, UserRecord>();

        public ValueTask Create(UserRecord record)
        {
            _store.TryAdd(record.Id, record);
            return new ValueTask();
        }

        public ValueTask<UserRecord> GetByEmail(string email)
        {
            return new ValueTask<UserRecord>(_store.Values.FirstOrDefault(x => x.Email == email));
        }
    }
}
