using System.Threading.Tasks;

namespace BddShop.Infra.Adapters
{
    public interface IUserStore
    {
        ValueTask Create(UserRecord record);
        ValueTask<UserRecord> GetByEmail(string email);
    }

    public class UserRecord
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
