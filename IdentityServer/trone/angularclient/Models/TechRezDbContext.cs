using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

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
        public DbSet<Feed> Feed { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
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

    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder) 
        {
            var categories = new Category[]
            {
                new Category()
                {
                    Code = "1",
                    Description = "Power Suply"
                },
                new Category()
                {
                    Code = "2",
                    Description = "Motherboard"
                },
                new Category()
                {
                    Code = "3",
                    Description = "Graphics Card"
                },
                new Category()
                {
                    Code = "4",
                    Description = "RAM"
                },
            };

            var products = new Product[]
            {
                //PSUs
                new Product()
                {
                    Code = "11",
                    Description = "Corsair RM850x",
                    Quantity = 52,
                    CategoryId = 1,
                    PhotoUrl = "PSU/Corsair_RM850x.jpg",
                    Price = 139,
                },
                new Product()
                {
                    Code = "12",
                    Description = "Cooler Master MasterWatt 750W",
                    Quantity = 87,
                    CategoryId = 1,
                    PhotoUrl = "PSU/Cooler_Master_MasterWatt_750W.jpg",
                    Price = 187,
                },
                new Product()
                {
                    Code = "13",
                    Description = "FSP Dagger 500W",
                    Quantity = 65,
                    CategoryId = 1,
                    PhotoUrl = "PSU/FSP_Dagger_500W.jpg",
                    Price = 184,
                },
                new Product()
                {
                    Code = "14",
                    Description = "Gamdias Astrape P1-750G",
                    Quantity = 65,
                    CategoryId = 1,
                    PhotoUrl = "PSU/Gamdias_Astrape_P1_750G.jpg",
                    Price = 227,
                },
                new Product()
                {
                    Code = "15",
                    Description = "NZXT E850",
                    Quantity = 86,
                    CategoryId = 1,
                    PhotoUrl = "PSU/NZXT_E850.jpg",
                    Price = 89,
                },
                new Product()
                {
                    Code = "16",
                    Description = "Seasonic Prime 1000 Titanium",
                    Quantity = 86,
                    CategoryId = 1,
                    PhotoUrl = "PSU/Seasonic_Prime_1000_Titanium.jpg",
                    Price = 299,
                },

                //mobos
                new Product()
                {
                    Code = "21",
                    Description = "Gigabyte Z390 Aorus Ultra",
                    Quantity = 63,
                    CategoryId = 2,
                    PhotoUrl = "MOB/Gigabyte_Z390_Aorus_Ultra.jpg",
                    Price = 250,
                },
                new Product()
                {
                    Code = "22",
                    Description = "ASUS ROG Maximus XI Hero Wi-Fi",
                    Quantity = 41,
                    CategoryId = 2,
                    PhotoUrl = "MOB/ASUS_ROG_Maximus_XI_Hero_Wi_Fi.jpg",
                    Price = 178,
                },
                new Product()
                {
                    Code = "23",
                    Description = "ASUS ROG Strix Z390-I Gaming",
                    Quantity = 85,
                    CategoryId = 2,
                    PhotoUrl = "MOB/ASUS_ROG_Strix_Z390_I_Gaming.jpg",
                    Price = 233,
                },
                new Product()
                {
                    Code = "24",
                    Description = "MSI MPG X570 Gaming Pro Carbon WiFi",
                    Quantity = 34,
                    CategoryId = 2,
                    PhotoUrl = "MOB/MSI_MPG_X570_Gaming_Pro_Carbon_WiFi.jpg",
                    Price = 189,
                },
                new Product()
                {
                    Code = "25",
                    Description = "Gigabyte X470 Aorus Gaming 5 Wi-Fi",
                    Quantity = 64,
                    CategoryId = 2,
                    PhotoUrl = "MOB/Gigabyte_X470_Aorus_Gaming_5_Wi_Fi.jpg",
                    Price = 175,
                },
                new Product()
                {
                    Code = "26",
                    Description = "ASUS TUF H370-Pro Gaming Wi-Fi",
                    Quantity = 16,
                    CategoryId = 2,
                    PhotoUrl = "MOB/ASUS_TUF_H370_Pro_Gaming_Wi_Fi.jpg",
                    Price = 199,
                },
                new Product()
                {
                    Code = "27",
                    Description = "Gigabyte Z390 UD",
                    Quantity = 52,
                    CategoryId = 2,
                    PhotoUrl = "MOB/Gigabyte_Z390_UD.jpg",
                    Price = 245,
                },

                //Graphics cards
                new Product()
                {
                    Code = "30",
                    Description = "Nvidia GeForce RTX 2080 Ti",
                    Quantity = 45,
                    CategoryId = 3,
                    PhotoUrl = "GFX/Nvidia_GeForce_RTX_2080_Ti.jpg",
                    Price = 1088,
                },
                new Product()
                {
                    Code = "31",
                    Description = "AMD Radeon RX 570 4GB",
                    Quantity = 88,
                    CategoryId = 3,
                    PhotoUrl = "GFX/AMD_Radeon_RX_570_4GB.jpg",
                    Price = 1072,
                },
                new Product()
                {
                    Code = "32",
                    Description = "Nvidia GeForce RTX 2080 Super",
                    Quantity = 32,
                    CategoryId = 3,
                    PhotoUrl = "GFX/Nvidia_GeForce_RTX_2080_Super.jpg",
                    Price = 860,
                },
                new Product()
                {
                    Code = "33",
                    Description = "Nvidia GeForce RTX 2070 Super",
                    Quantity = 56,
                    CategoryId = 3,
                    PhotoUrl = "GFX/Nvidia_GeForce_RTX_2070_Super.jpg",
                    Price = 890,
                },
                new Product()
                {
                    Code = "34",
                    Description = "Nvidia GeForce RTX 2060 Super",
                    Quantity = 73,
                    CategoryId = 3,
                    PhotoUrl = "GFX/Nvidia_GeForce_RTX_2060_Super.jpg",
                    Price = 699,
                },
                new Product()
                {
                    Code = "35",
                    Description = "AMD Radeon RX 5700 XT",
                    Quantity = 81,
                    CategoryId = 3,
                    PhotoUrl = "GFX/AMD_Radeon_RX_5700_XT.jpg",
                    Price = 599,
                },
                new Product()
                {
                    Code = "36",
                    Description = "GeForce GTX 1660 Super 6GB",
                    Quantity = 47,
                    CategoryId = 3,
                    PhotoUrl = "GFX/GeForce_GTX_1660_Super_6GB.jpg",
                    Price = 255,
                },
                new Product()
                {
                    Code = "37",
                    Description = "AMD Radeon RX 590",
                    Quantity = 86,
                    CategoryId = 3,
                    PhotoUrl = "GFX/AMD_Radeon_RX_590.jpg",
                    Price = 430,
                },
                new Product()
                {
                    Code = "38",
                    Description = "AMD Radeon RX 5700",
                    Quantity = 38,
                    CategoryId = 3,
                    PhotoUrl = "GFX/AMD_Radeon_RX_5700.jpg",
                    Price = 230,
                },
                new Product()
                {
                    Code = "39",
                    Description = "Nvidia GeForce GTX 1660 Ti",
                    Quantity = 76,
                    CategoryId = 3,
                    PhotoUrl = "GFX/Nvidia_GeForce_GTX_1660_Ti.jpg",
                    Price = 245,
                },

                //Ram
                new Product()
                {
                    Code = "40",
                    Description = "Corsair Vengeance LED",
                    Quantity = 65,
                    CategoryId = 4,
                    PhotoUrl = "RAM/Corsair_Vengeance_LED.jpg",
                    Price = 89,
                },
                new Product()
                {
                    Code = "41",
                    Description = "G.Skill Trident Z RGB",
                    Quantity = 65,
                    CategoryId = 4,
                    PhotoUrl = "RAM/G_Skill_Trident_Z_RGB.jpg",
                    Price = 99,
                },
                new Product()
                {
                    Code = "42",
                    Description = "Kingston HyperX Predator",
                    Quantity = 71,
                    CategoryId = 4,
                    PhotoUrl = "RAM/Kingston_HyperX_Predator.jpg",
                    Price = 199,
                },
                new Product()
                {
                    Code = "43",
                    Description = "Kingston HyperX Fury ",
                    Quantity = 63,
                    CategoryId = 4,
                    PhotoUrl = "RAM/Kingston_HyperX_Fury .jpg",
                    Price = 140,
                },
                new Product()
                {
                    Code = "44",
                    Description = "Corsair Dominator Platinum RGB",
                    Quantity = 85,
                    CategoryId = 4,
                    PhotoUrl = "RAM/Corsair_Dominator_Platinum_RGB.jpg",
                    Price = 175,
                },
                new Product()
                {
                    Code = "45",
                    Description = "G.Skill Trident Z RGB DC",
                    Quantity = 27,
                    CategoryId = 4,
                    PhotoUrl = "RAM/G_Skill_Trident_Z_RGB_DC.jpg",
                    Price = 146,
                },
                new Product()
                {
                    Code = "46",
                    Description = "Adata Spectrix D80 ",
                    Quantity = 19,
                    CategoryId = 4,
                    PhotoUrl = "RAM/Adata_Spectrix_D80.jpg",
                    Price = 99,
                },
                new Product()
                {
                    Code = "47",
                    Description = "HyperX Fury RGB",
                    Quantity = 36,
                    CategoryId = 4,
                    PhotoUrl = "RAM/HyperX_Fury_RGB.jpg",
                    Price = 187,
                },
                new Product()
                {
                    Code = "48",
                    Description = "Corsair Vengeance LPX",
                    Quantity = 85,
                    CategoryId = 4,
                    PhotoUrl = "RAM/Corsair_Vengeance_LPX.jpg",
                    Price = 163,
                },
                new Product()
                {
                    Code = "49",
                    Description = "G.Skill Mac RAM",
                    Quantity = 26,
                    CategoryId = 4,
                    PhotoUrl = "RAM/G_Skill_Mac_RAM.jpg",
                    Price = 165,
                },
            };

            var techRezUsers = new TechRezUser[]
            {
                new TechRezUser()
                {
                    Code = "RandomDude_1",
                    UserName = "RandomDude One",
                },
                new TechRezUser()
                {
                    Code = "RandomDude_2",
                    UserName = "RandomDude Two",
                },
            };

            var orders = new Order[]
            {
                new Order()
                {
                    Code = "1",
                    OrderDate = DateTime.Now.AddDays(-1).AddHours(-2).AddMinutes(3).AddSeconds(4),
                    ProductId = 15,
                    TechRezUserId = "RandomDude_1",
                    Quantity = 1
                },
                new Order()
                {
                    Code = "2",
                    OrderDate = DateTime.Now.AddDays(-5).AddHours(-6).AddMinutes(7).AddSeconds(8),
                    ProductId = 24,
                    TechRezUserId = "RandomDude_2",
                    Quantity = 2
                },
            };

            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Product>().HasData(products);
            modelBuilder.Entity<TechRezUser>().HasData(techRezUsers);
            modelBuilder.Entity<Order>().HasData(orders);
        }
    }
}
