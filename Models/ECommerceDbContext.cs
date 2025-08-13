using Microsoft.EntityFrameworkCore;
using ECommerce.Models;

namespace ECommerce.Models
{
    public class ECommerceDbContext:DbContext
    {
        public ECommerceDbContext()
        {
            
        }
        public ECommerceDbContext(DbContextOptions options):base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;database=ECommerceMVC;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Category>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Payment>()
               .Property(p => p.PaymentDate)
               .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>()
               .Property(p => p.CreatedAt)
               .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<User>()
              .Property(u => u.CreatedAt)
              .HasDefaultValueSql("GETDATE()");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }



    }
}
