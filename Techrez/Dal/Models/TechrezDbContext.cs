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
                new Category() { Id = 2, Description = "Graphics card" }
            );

            modelBuilder.Entity<Product>().HasData
            (
                new Product { Id = 1, CategoryID = 1, Description = "Earthwatts 500", Price = 56, Stock = 23 },
                new Product { Id = 2, CategoryID = 1, Description = "EVGA 850", Price = 80, Stock = 14 },
                new Product { Id = 3, CategoryID = 1, Description = "Cooler master 750", Price = 69, Stock = 8 },
                new Product { Id = 4, CategoryID = 2, Description = "Intel 3750k", Price = 250, Stock = 6 },
                new Product { Id = 5, CategoryID = 2, Description = "Intel 4770k", Price = 320, Stock = 11 }
            );
        }
    }
}
