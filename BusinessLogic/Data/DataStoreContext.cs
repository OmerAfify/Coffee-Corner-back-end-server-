using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinesssLogic.Data
{  public class DataStoreContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }
        public DbSet<OrderDeliveryMethods> DeliveryMethods { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }

        public DataStoreContext(DbContextOptions<DataStoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(e => { 
                    e.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
                    e.Property(p => p.ProductDescription).HasMaxLength(200);
                    e.Property(p => p.SalesPrice).HasColumnType("decimal(18,2)");
                    });  
            
            modelBuilder.Entity<Category>(e => { 
                    e.Property(p => p.CategoryName).IsRequired().HasMaxLength(100);
                    });  
            
            modelBuilder.Entity<ProductBrand>(e => { 
                    e.Property(p => p.ProductBrandName).IsRequired().HasMaxLength(100);
                    });


            modelBuilder.Entity<Order>().OwnsOne(sa => sa.ShippingAddress, a => { a.WithOwner(); });
            modelBuilder.Entity<Order>().Property(p => p.SubTotal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(p => p.Total).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().HasMany(o => o.OrderedItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderedItem>().OwnsOne(i => i.ProductItemOrdered, pio => { pio.WithOwner(); });
            modelBuilder.Entity<OrderedItem>().Property(p => p.TotalPrice).HasColumnType("decimal(18,2)");

        } 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {


            base.OnConfiguring(optionBuilder);

        }

    }
  
}
