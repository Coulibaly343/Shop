using Microsoft.EntityFrameworkCore;
using Shop.Core.Domains;
using Shop.Core.Domains.Abstract;

namespace Shop.Infrastructure.Data {
    public class ShopContext : DbContext {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public ShopContext (DbContextOptions<ShopContext> options) : base (options) { }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Account> ()
                .HasOne (a => a.Address)
                .WithOne (b => b.Account)
                .HasForeignKey<Address> (b => b.AccountId);
        }
    }
}