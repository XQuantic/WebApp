using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneStore.Models
{
    public interface IRepository
    {
        Task<List<Phone>> GetPhones();
        Task<Phone> GetPhone(string name);
        Task<List<Company>> GetCompanies();
        Task RemovePhone(Phone phone);
        Task InsertPhone(Phone phone);
        Task<User> GetUser(string email, string password);
        Task<User> GetUser(string email);
        Task<Role> GetRole(string roleName);
        Task InsertUser(User user);
    }
}