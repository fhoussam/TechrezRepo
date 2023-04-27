using jh;
using jh.Entities;
using jh.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace angularclient
{
    public class JhDbContext : DbContext, IJhDbContext
    {
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<ResultEntity> ResultEntities { get; set; }
        public DbSet<UrlSpecialCharacter> UrlSpecialCharacters { get; set; }
        public DbSet<SearchJob> SearchJobs { get; set; }
        public DbSet<DescriptionUrlTransformer> DescriptionUrlTransformers { get; set; }
        public DbSet<CachedUrl> CachedUrls { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Provider>().Property(e => e.ProviderId).ValueGeneratedNever();
            modelBuilder.Entity<UrlSpecialCharacter>().Property(e => e.UrlSpecialCharacterId).ValueGeneratedNever();
            modelBuilder.Entity<Keyword>().Property(e => e.KeywordId).ValueGeneratedNever();

            modelBuilder.Entity<Provider>().HasData(InitData.Providers);
            modelBuilder.Entity<UrlSpecialCharacter>().HasData(InitData.UrlSpecialCharacters);
            modelBuilder.Entity<Keyword>().HasData(InitData.Keywords);
        }

        public JhDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
