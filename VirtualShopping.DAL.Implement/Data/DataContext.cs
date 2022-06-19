using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualShopping.Domain.Entities;

namespace VirtualShopping.DAL.Implement.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Shop> Shops { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartsItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>()
                .Property(c => c.Avatar)
                .IsUnicode(false);

            builder.Entity<Customer>()
                .Property(c => c.CustomerId)
                .IsUnicode(false);

            builder.Entity<Customer>()
                .Property(c => c.PhoneNumber)
                .IsUnicode(false);

            builder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique(true);

            builder.Entity<Shop>()
                .HasIndex(s => s.PhoneNumber)
                .IsUnique(true);

            builder.Entity<Cart>()
                .Property(c => c.CartId)
                .IsUnicode(false);

            builder.Entity<Cart>()
                .Property(c => c.ShopId)
                .IsUnicode(false);

            builder.Entity<Shop>()
                .Property(s => s.ShopId)
                .IsUnicode(false);

            builder.Entity<CartItem>()
                .Property(c => c.CartId)
                .IsUnicode(false);
        }
    }
}
