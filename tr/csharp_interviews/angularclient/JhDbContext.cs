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
            modelBuilder.Entity<Provider>().HasData(
                    new Provider()
                    {
                        ProviderId = 1,
                        ListUrl = $"https://fr.indeed.com/jobs?q={AppSettings.KeywordReplacePattern}&l=%C3%8Ele-de-France&sort=date",
                        UrlPath = "//div[contains(@class, 'cardOutline')]/div/div[1]/div/div/table/tbody/tr/td/div/h2/a/@href",
                        TitlePath = "//div[contains(@class, 'cardOutline')]/div/div[1]/div/div/table/tbody/tr/td/div/h2/a",
                        DatePath = "//div[contains(@class, 'cardOutline')]/div/div[1]/div/div/table[2]/tbody/tr[2]/td/div/span[1]/text()",
                        PublisherPath = "//div[contains(@class, 'companyInfo')]/span[1]",
                        DescriptionPath = "//div[contains(@id, 'jobDescriptionText')]",
                        EmptyPageIndicatorPath = "//div[contains(@class, 'jobsearch-NoResult-messageHeader')]",
                    }
                    //,new Provider()
                    //{
                    //    ProviderId = 2,
                    //    ListUrl = $"https://www.free-work.com/fr/tech-it/jobs?query={AppSettings.KeywordReplacePattern}&locations=fr~ile-de-france~~&sort=date",
                    //    UrlPath = "//div[contains(@class, 'rounded-lg')]/div/h2/a/@href",
                    //    TitlePath = "//div[contains(@class, 'rounded-lg')]/div/h2/a",
                    //    DatePath = "//div[contains(@class, 'rounded-lg')]/div/div[2]/div[2]",
                    //    PublisherPath = "//div[contains(@class, 'rounded-lg')]/div/div[3]/div/div[1]",
                    //    DescriptionPath = "//div[contains(@class, 'grid')]/div/div[2]",
                    //    EmptyPageIndicatorPath = "//div[contains(@class, 'text-center') and contains(@class, 'pt-12') and contains(@class, 'pb-6')]/div/div[2]",
                    //    IsJobIdInQueryParam = true,
                    //}
                );

            modelBuilder.Entity<UrlSpecialCharacter>().HasData(
                    new UrlSpecialCharacter()
                    {
                        UrlSpecialCharacterId = 1,
                        Value = '#',
                        Replacer = "%23",
                        ProviderId = 1
                    }
                    //,new UrlSpecialCharacter()
                    //{
                    //    UrlSpecialCharacterId = 2,
                    //    Value = '#',
                    //    Replacer = "%23",
                    //    ProviderId = 2
                    //}
                );

            //modelBuilder.Entity<DescriptionUrlTransformer>().HasData(
            //        new DescriptionUrlTransformer()
            //        {
            //            DescriptionUrlTransformerId = 2,
            //            Value = "rc/clk",
            //            Replacer = "voir-emploi",
            //            ProviderId = 2
            //        }
            //    );

            modelBuilder.Entity<Keyword>().HasData(
                    //new Keyword(01, "wcf"),
                    new Keyword(02, "asp"),
                    //new Keyword(03, "c#"),
                    //new Keyword(04, "csharp"),
                    //new Keyword(05, "dotnet"),
                    //new Keyword(06, "asp.net"),
                    //new Keyword(07, "jquery", false),
                    //new Keyword(08, "core", false),
                    //new Keyword(09, "angular", false),
                    new Keyword(10, "anglais", false)
                    //,new Keyword(11, "english", false),
                    //new Keyword(12, "senior", false),
                    //new Keyword(13, "openid", false),
                    //new Keyword(14, "oauth", false),
                    //new Keyword(15, "mssql", false),
                    //new Keyword(16, "identity", false),
                    //new Keyword(17, "visual studio", false),
                    //new Keyword(18, "sql server", false),
                    //new Keyword(19, "windows communication foundation", false),
                    //new Keyword(20, "aws", false),
                    //new Keyword(21, "docker", false),
                    //new Keyword(22, "webform"),
                    //new Keyword(23, "winform"),
                    //new Keyword(24, "asmx")
                );
        }

        public JhDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
