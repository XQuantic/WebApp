using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneStore.Models
{
    public interface IRepository
    {
        Task<List<Phone>> GetPhones();
        Task<List<Company>> GetCompanies();
        Task<Phone> GetPhone(string name);
        Task RemovePhone(Phone phone);
        Task SavePhone(Phone phone);
        Task<User> GetUser(string email, string password);
        Task<User> GetUser(string email);
        Task<Role> GetRole(string roleName);
        Task SaveUser(User user);
    }
}