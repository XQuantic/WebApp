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
            return await _db.Phones.Include(x => x.Company)
                .ToListAsync();
        }

        public async Task<Phone> GetPhone(string name)
        {
            return await _db.Phones.FirstOrDefaultAsync(x => x.Name == name);
        }
        public async Task<List<Company>> GetCompanies()
        {
            return await _db.Companies.ToListAsync();
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
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetUser(string email, string password)
        {
            return await _db.Users.Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
        public async Task SaveUser(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }
        public async Task<Role> GetRole(string roleName)
        {
            return await _db.Roles.FirstOrDefaultAsync(x => x.Name == roleName);
        }
    }
}