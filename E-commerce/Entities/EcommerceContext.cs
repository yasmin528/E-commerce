using E_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Entities
{
    public class EcommerceContext :DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Carts> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-0C7I0PS;Database=E-commerceDB;Trusted_Connection=True; Encrypt= false");
        }
    }
}
