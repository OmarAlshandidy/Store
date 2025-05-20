using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Data.Context
{
    public class StoreDbContext:DbContext 
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DelivaryMethods { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>().HasData(new Product() { });

        }

    }
}
