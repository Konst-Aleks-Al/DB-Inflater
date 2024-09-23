using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace App.Helpers {

    public class DatabaseContext : DbContext {
        
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        public DatabaseContext() {
            Database.EnsureCreated();
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=onlineShop;Username=postgres;Password=1044;Include Error Detail=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder
                .Entity<OrderProduct>()
                .HasKey(op => new {op.OrderId, op.ProductId});
        }
    }
}
