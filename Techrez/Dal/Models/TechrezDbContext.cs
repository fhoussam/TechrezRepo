using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dal.Models
{
    public class TechrezDbContext : DbContext
    {
        public TechrezDbContext(DbContextOptions<TechrezDbContext> options) : base(options)
        {
             
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData
            (
                new Category() { Id = 1, Description = "Power supply unit" },
                new Category() { Id = 2, Description = "CPU" },
                new Category() { Id = 3, Description = "Graphics card" }
            );

            modelBuilder.Entity<Product>().HasData
            (
                new Product { Id = 01, CategoryID = 1, Description = "Earthwatts 500w", Price = 56, Stock = 23 },
                new Product { Id = 02, CategoryID = 1, Description = "EVGA 850w", Price = 80, Stock = 14 },
                new Product { Id = 03, CategoryID = 1, Description = "Cooler master 750w", Price = 69, Stock = 8 },
                new Product { Id = 04, CategoryID = 1, Description = "Corsair 850W", Price = 250, Stock = 16 },
                new Product { Id = 05, CategoryID = 1, Description = "BeQuiet! 450W", Price = 250, Stock = 16 },
                new Product { Id = 06, CategoryID = 1, Description = "Enermax 600W", Price = 250, Stock = 16 },
                new Product { Id = 07, CategoryID = 2, Description = "Intel i5 3750k", Price = 250, Stock = 31 },
                new Product { Id = 08, CategoryID = 2, Description = "Intel i7 4770k", Price = 320, Stock = 15 },
                new Product { Id = 09, CategoryID = 2, Description = "Intel i7 3770k", Price = 299, Stock = 6 },
                new Product { Id = 10, CategoryID = 2, Description = "Intel Quad Q6600", Price = 80, Stock = 8 },
                new Product { Id = 11, CategoryID = 2, Description = "Intel i5 3550K", Price = 130, Stock = 7 },
                new Product { Id = 12, CategoryID = 3, Description = "Nvidia 980 GTX", Price = 280, Stock = 4 },
                new Product { Id = 13, CategoryID = 3, Description = "MSI 1080 ti Gaming", Price = 550, Stock = 20 },
                new Product { Id = 14, CategoryID = 3, Description = "MSI 970 GTX Gaming", Price = 199, Stock = 78 },
                new Product { Id = 15, CategoryID = 3, Description = "Asus 1080 GTX Strix OC", Price = 449, Stock = 26 },
                new Product { Id = 16, CategoryID = 3, Description = "Gigabyte 1060 GTX Windforce", Price = 220, Stock = 13 }
            );
        }
    }
}
