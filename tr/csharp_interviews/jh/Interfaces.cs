using HtmlAgilityPack;
using jh.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace jh.Interfaces
{
    public interface Interfaces
    {
        List<Keyword> Keywords { get; set; }
        List<Provider> Providers { get; set; }
        List<ResultEntity> PreviousResult { get; set; }
    }

    public interface IHelper
    {
        HtmlDocument DownloadHtml(string url);
        XmlDocument ToXml(string html);
    }

    public interface IDateParser
    {
        DateTime ParseDate(string input);
    }

    public interface IJhDbContext
    {
        DbSet<Keyword> Keywords { get; set; }
        DbSet<Provider> Providers { get; set; }
        DbSet<ResultEntity> ResultEntities { get; set; }
        DbSet<CachedUrl> CachedUrls { get; set; }
        DbSet<SearchJob> SearchJobs { get; set; }
        DbSet<UrlSpecialCharacter> UrlSpecialCharacters { get; set; }
        DbSet<DescriptionUrlTransformer> DescriptionUrlTransformers { get; set; }
        int SaveChanges();
    }
}
