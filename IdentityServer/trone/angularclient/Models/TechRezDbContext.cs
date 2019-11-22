using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient.Models
{
    public class TechRezDbContext : DbContext
    {
        public TechRezDbContext(DbContextOptions<TechRezDbContext> options)
            : base(options)
        { }

        public DbSet<TechRezUser> TechRezUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
    }

    public class TechRezDbContextFactory : IDesignTimeDbContextFactory<TechRezDbContext>
    {
        //private  IConfiguration _configuration { get; }

        //public TechRezDbContextFactory(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        TechRezDbContext IDesignTimeDbContextFactory<TechRezDbContext>.CreateDbContext(string[] args)
        {
            string connectionString =
                "Server=\\SQLEXPRESS;Database=techrezdb;Trusted_Connection=True;MultipleActiveResultSets=true".
                Replace("=\\", "=" + System.Environment.MachineName + "\\");

            var optionsBuilder = new DbContextOptionsBuilder<TechRezDbContext>();
            optionsBuilder.UseSqlServer<TechRezDbContext>(connectionString);
            return new TechRezDbContext(optionsBuilder.Options);
        }
    }
}
