using CookieClicker.ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ControllerForCookieClicker.Different
{
    public class DataBaseCookieContext : DbContext
    {
        //public DataBaseCookieContext(DbContextOptions options) : base(options) { }
        public DataBaseCookieContext() { }
        public DbSet<Account> Account { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<DonateStatus> DonateStatus { get; set; }
        public DbSet<Donation> Donation { get; set; }
        public DbSet<Enhancement> Enhancement { get; set; }
        public DbSet<EnhancementAccount> EnhancementAccount { get; set; }
        public DbSet<Friend> Friend { get; set; }
        public DbSet<ImageIB> ImageIB { get; set; }
        public DbSet<TypeEnhancement> TypeEnhancement { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Startup.Configuration.GetConnectionString("Myconnection"));
        }
    }
}
