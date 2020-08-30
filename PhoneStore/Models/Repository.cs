using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PhoneStore.Models
{
    public class Repository : IRepository
    {
        private readonly MyDbContext _db;
        public Repository(MyDbContext db)
        {
            _db = db;
        }
        public async Task<List<Phone>> GetPhones()
        {
            return await _db.Phones.Include(phone => phone.Company).ToListAsync();
        }
        public Task<List<Company>> GetCompanies()
        {

            return _db.Companies.Select(company => company).ToListAsync();
        }
        public async Task<Phone> GetPhone(string name)
        {
            return await _db.Phones.FirstOrDefaultAsync(phone => phone.Name == name);
        }

        public async Task<Phone> GetPhoneId(int id)
        {
            return await _db.Phones.FirstOrDefaultAsync(phone => phone.Id == id);
        }

        public async Task RemovePhone(Phone phone)
        {
            _db.Phones.Remove(phone);
            await _db.SaveChangesAsync();
        }
        public async Task SavePhone(Phone phone)
        {
            await _db.Phones.AddAsync(phone);
            await _db.SaveChangesAsync();
        }
        public async Task<User> GetUser(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(user => user.Email == email);
        }
        public async Task<User> GetUser(string email, string password)
        {
            return await _db.Users.Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Email == email && user.Password == password);
        }
        public async Task SaveUser(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }
        public async Task<Role> GetRole(string roleName)
        {
            return await _db.Roles.FirstOrDefaultAsync(role => role.Name == roleName);
        }
    }
}