using System.Threading.Tasks;

namespace BddShop.Infra.Adapters
{
    public interface IUserStore
    {
        ValueTask Create(UserRecord record);
    }

    public class UserRecord
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
