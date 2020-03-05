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
                        ListUrl = $"https://www.freelance-info.fr/missions.php?f=ile_de_france&mots={AppSettings.KeywordReplacePattern}&tri=date&p={AppSettings.PageIndexReplacePattern}",
                        UrlPath = "//div[contains(@class, 'roffre')]/div/div[2]/div[1]/a",
                        TitlePath = "//div[contains(@class, 'roffre')]/div/div[2]/div[1]",
                        DatePath = "//div[contains(@class, 'roffre')]/div/div[2]/span[2]",
                        PublisherPath = "//div[contains(@class, 'roffre')]/div/div[1]/div/img",
                        DescriptionPath = "//div[@id = 'divcontmain-pad']/div[2]/div[3]",
                        EmptyPageIndicatorPath = "//div[contains(@class, 'roffre')]",
                    },
                    new Provider()
                    {
                        ProviderId = 2,
                        ListUrl = $"https://www.indeed.fr/emplois?as_and={AppSettings.KeywordReplacePattern}&as_phr=&as_any=&as_not=&as_ttl=&as_cmp=&jt=subcontract&st=&as_src=&salary=&radius=25&l=%C3%8Ele-de-France&fromage=any&limit=50&sort=date&psf=advsrch&from=advancedsearch",
                        UrlPath = "//div[contains(@class, 'title')]/a",
                        TitlePath = "//div[contains(@class, 'title')]",
                        DatePath = "//div[contains(@class, 'result-link-bar')]/div/span[contains(@class, 'date')]",
                        PublisherPath = "//div[contains(@class, 'sjcl')]/div/span[contains(@class, 'company')]",
                        DescriptionPath = "//div[contains(@class, 'jobsearch-JobComponent-description')]",
                        EmptyPageIndicatorPath = "//div[contains(@class, 'jobsearch-SerpJobCard')]",
                        IsJobIdInQueryParam = true,
                    }
                );

            modelBuilder.Entity<UrlSpecialCharacter>().HasData(
                    new UrlSpecialCharacter()
                    {
                        UrlSpecialCharacterId = 1,
                        Value = '#',
                        Replacer = "%23",
                        ProviderId = 1
                    },
                    new UrlSpecialCharacter()
                    {
                        UrlSpecialCharacterId = 2,
                        Value = '#',
                        Replacer = "%23",
                        ProviderId = 2
                    }
                );

            modelBuilder.Entity<DescriptionUrlTransformer>().HasData(
                    new DescriptionUrlTransformer()
                    {
                        DescriptionUrlTransformerId = 2,
                        Value = "rc/clk",
                        Replacer = "voir-emploi",
                        ProviderId = 2
                    }
                );

            modelBuilder.Entity<Keyword>().HasData(
                    new Keyword(01, "wcf"),
                    new Keyword(02, "asp"),
                    new Keyword(03, "c#"),
                    new Keyword(04, "csharp"),
                    new Keyword(05, "dotnet"),
                    new Keyword(06, "asp.net"),
                    new Keyword(07, "jquery", false),
                    new Keyword(08, "core", false),
                    new Keyword(09, "angular", false),
                    new Keyword(10, "anglais", false),
                    new Keyword(11, "english", false),
                    new Keyword(12, "senior", false),
                    new Keyword(13, "openid", false),
                    new Keyword(14, "oauth", false),
                    new Keyword(15, "mssql", false),
                    new Keyword(16, "identity", false),
                    new Keyword(17, "visual studio", false),
                    new Keyword(18, "sql server", false),
                    new Keyword(19, "windows communication foundation", false),
                    new Keyword(20, "aws", false),
                    new Keyword(21, "docker", false),
                    new Keyword(22, "webform"),
                    new Keyword(23, "winform"),
                    new Keyword(24, "asmx")
                );
        }

        public JhDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
