using HtmlAgilityPack;
using jh.Entities;
using jh.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace jh
{
    public class Search
    {
        private IHelper _helper { get; set; }
        public Dictionary<string, SearchResultEntity> Result { get; private set; }
        public List<CachedUrl> CurrentCachedUrls { get; private set; }
        public SearchState SearchState { get; private set; }
        private readonly IJhDbContext _jhDbContext;

        public Search(
            //IStore store, 
            IHelper helper, IJhDbContext jhDbContext)
        {
            //_store = store;
            _jhDbContext = jhDbContext;
            _helper = helper;
            SearchState = SearchState.Ping;
            Result = new Dictionary<string, SearchResultEntity>();
            CurrentCachedUrls = new List<CachedUrl>();
        }

        public void Start()
        {
            //Ping

            //Search
            Process();

            //match
            SearchState = SearchState.Match;

            int i = 0;
            HashSet<string> NotFoundPaths = new HashSet<string>();
            foreach (var searchResult in Result)
            {
                i++;
                int progressPourcentage = i * 100 / Result.Count;
                string progress = $"{progressPourcentage} %";
                try
                {
                    var cachedUrl = InitDescription(searchResult);
                    CurrentCachedUrls.Add(cachedUrl);
                }
                catch (EntityResultNoLongerAvailableException)
                {
                    NotFoundPaths.Add(searchResult.Key);
                }
            }
            Result = Result.Where(x => !NotFoundPaths.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);

            //Save
            Save();

            //Complete
            SearchState = SearchState.Complete;
        }

        private void Save()
        {
            var searchJob = new SearchJob() { SearchDate = DateTime.Now };
            var requiredComplexeKeywords = _jhDbContext.Keywords.ToList().Where(x => x.IsComplexe || x.IsRequired).ToList();

            Result = Result.Where(x => x.Value.GetRequiredMatches(requiredComplexeKeywords).Count > 0).ToDictionary(x => x.Key, x => x.Value);

            CurrentCachedUrls.ForEach(x => x.SearchJob = searchJob);
            _jhDbContext.SearchJobs.Add(searchJob);
            _jhDbContext.ResultEntities.AddRange(Result.Select(x => new ResultEntity()
            {
                Description = x.Value.Description,
                Path = x.Key,
                Provider = x.Value.Provider,
                PublishDate = x.Value.PublishDate,
                Publisher = x.Value.Publisher,
                RefUrl = x.Value.RefUrl,
                SearchJob = searchJob,
                Title = x.Value.Title,
            }));
            _jhDbContext.CachedUrls.AddRange(CurrentCachedUrls);
            _jhDbContext.SaveChanges();
        }

        private void Process()
        {
            var previousResult = _jhDbContext.ResultEntities.Select(x => x.Path).ToList();
            var cachedUrls = _jhDbContext.CachedUrls.Select(x => x.Path).ToList();
            var UrlsToIgnore = new List<string>();
            UrlsToIgnore.AddRange(previousResult);
            UrlsToIgnore.AddRange(cachedUrls);

            var keywords = _jhDbContext.Keywords.ToList().Where(x => !x.IsComplexe //add param for providers who support complexe keywords!
                //&& x.Value == "c#"
                ).ToList();
            var providers = _jhDbContext.Providers
                .ToList();

            var forbiddenUrlWords = new List<string>() { "pagead" };

            //should be repaced with Include later
            var descriptionUrlTransformers = _jhDbContext.DescriptionUrlTransformers.ToList();
            var urlSpecialCharacters = _jhDbContext.UrlSpecialCharacters.ToList();
            providers.ForEach(x =>
            {
                x.DescriptionUrlTransformer = descriptionUrlTransformers.Where(y => x.ProviderId == y.ProviderId).ToList();
                x.UrlSpecialCharacter = urlSpecialCharacters.Where(y => x.ProviderId == y.ProviderId).ToList();
            });

            foreach (var provider in providers)
            {
                var getParams = keywords.Select(x =>
                {
                    string getParam = x.Value;
                    //provider.UrlSpecialCharacter
                    provider.UrlSpecialCharacter.Where(x => x.ProviderId == provider.ProviderId).ToList().ForEach((y) =>
                      {
                          getParam = getParam.Replace(y.Value.ToString(), y.Replacer);
                      });
                    return getParam;
                });

                foreach (var getParam in getParams)
                {
                    for (int i = 1; i <= AppSettings.MaxPagesToScan; i++)
                    {
                        string url = provider.ListUrl
                            .Replace(AppSettings.KeywordReplacePattern, getParam)
                            .Replace(AppSettings.PageIndexReplacePattern, i.ToString());

                        try
                        {
                            var htmlDocument = _helper.Download(url);
                            var iterationResult = ExtractResultEntitie(htmlDocument, provider);
                            iterationResult.ForEach(x =>
                            {
                                if (!provider.IsJobIdInQueryParam)
                                    x.Path = x.Path.Split('?')[0];

                                if (
                                    !UrlsToIgnore.Any(y => y == x.Path)
                                    && !Result.ContainsKey(x.Path)
                                    && !forbiddenUrlWords.Any(y => x.Path.ToLower().Contains(y.ToLower()))
                                )
                                {
                                    x.RefUrl = url;
                                    Result.Add(x.Path, new SearchResultEntity() 
                                    {
                                        Description = x.Description,
                                        Provider = x.Provider,
                                        PublishDate = x.PublishDate,
                                        Publisher = x.Publisher,
                                        RefUrl = x.RefUrl,
                                        Title = x.Title,
                                    });
                                }
                            });
                        }
                        catch (EmptyPageException)
                        {
                            break;
                        }
                        catch (Exception)
                        {
                            throw new ListUrlPingException();
                        }
                    }
                }
            }
        }

        private CachedUrl InitDescription(KeyValuePair<string, SearchResultEntity> resultEntity)
        {
            Uri providerListUrl = new Uri(resultEntity.Value.Provider.ListUrl);
            string url = $"http{(providerListUrl.Port == 443 ? "s" : string.Empty)}://{providerListUrl.Host}/{resultEntity.Key}";

            HtmlDocument descriptionHtmlDoc;

            try
            {
                descriptionHtmlDoc = _helper.Download(url);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("404"))
                    throw new EntityResultNoLongerAvailableException();

                throw new DescriptionPingException();
            }

            try
            {
                HtmlNode descriptionNode = descriptionHtmlDoc.DocumentNode.SelectSingleNode(resultEntity.Value.Provider.DescriptionPath);
                resultEntity.Value.Description = WebUtility.HtmlDecode(descriptionNode.InnerText);
                return new CachedUrl()
                {
                    Path = resultEntity.Key,
                    Provider = resultEntity.Value.Provider,
                };
            }
            catch (Exception)
            {
                throw new DescriptionPathException();
            }
        }

        private List<ResultEntity> ExtractResultEntitie(HtmlDocument xmlDocument, Provider provider)
        {
            var result = new List<ResultEntity>();
            List<string> titles, urls, rawDates, publishers;
            List<DateTime> dates = new List<DateTime>();

            try
            {
                xmlDocument.DocumentNode.SelectNodes(provider.EmptyPageIndicatorPath).ToList();
            }
            catch (Exception)
            {
                //should be logged
                throw new EmptyPageException();
            }

            try
            {
                titles = xmlDocument.DocumentNode.SelectNodes(provider.TitlePath).Select(x => WebUtility.HtmlDecode(x.InnerText.Trim())).ToList();
            }
            catch (Exception) { throw new TitlePathException(); }

            try
            {
                urls = xmlDocument.DocumentNode.SelectNodes(provider.UrlPath).Select(x =>
                {
                    string result = WebUtility.HtmlDecode(x.Attributes["href"].Value);
                    provider.DescriptionUrlTransformer.ForEach(x =>
                    {
                        result = result.Replace(x.Value, x.Replacer);
                    });
                    return result;
                }).ToList();
            }
            catch (Exception) { throw new ListUrlPathException(); }

            try
            {
                rawDates = xmlDocument.DocumentNode.SelectNodes(provider.DatePath).Select(x => WebUtility.HtmlDecode(x.InnerText.Trim())).ToList();
            }
            catch (Exception) { throw new ParseDateException(); }

            try
            {
                publishers = xmlDocument.DocumentNode.SelectNodes(provider.PublisherPath).Select(x =>
                {
                    if (x.Name == "img")
                        return x.Attributes["alt"].Value;
                    else
                        return WebUtility.HtmlDecode(x.InnerText.Trim());
                }).ToList();

                if (publishers.Count != rawDates.Count)
                {
                    string xpath = provider.PublisherPath.Replace("/img", "");
                    publishers = xmlDocument.DocumentNode.SelectNodes(xpath).Select(x =>
                    {
                        return WebUtility.HtmlDecode(x.InnerText.Trim());
                    }).ToList();
                }
            }
            catch (Exception) { throw new DescriptionPathException(); }

            if (
                titles.Count() != urls.Count()
                || urls.Count() != rawDates.Count()
                || titles.Count() != rawDates.Count()
                || rawDates.Count() != publishers.Count()
                )
            {
                throw new TagMismatchException();
            }

            var dateParser = DateParserFactory.CreateDateParser(provider.DateExtractor);

            try
            {
                dates = rawDates.Select(x => dateParser.ParseDate(x)).ToList();
            }
            catch (Exception)
            {
                throw new ParseDateException();
            }

            for (int i = 0; i < titles.Count(); i++)
            {
                result.Add(
                    new ResultEntity()
                    {
                        Path = urls[i],
                        Provider = provider,
                        PublishDate = dates[i],
                        Publisher = publishers[i],
                        Title = titles[i]
                    }
                );
            }

            return result;
        }
    }
}
