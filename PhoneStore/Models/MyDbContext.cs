using Microsoft.EntityFrameworkCore;

namespace PhoneStore.Models
{
    public sealed class MyDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<User> Users { get; set; }
        public MyDbContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ProjectSync");

        }
    }
}