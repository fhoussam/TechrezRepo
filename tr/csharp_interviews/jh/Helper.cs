using HtmlAgilityPack;
using jh.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace jh
{
    public class Helper : IHelper
    {
        public HtmlDocument DownloadHtml(string url)
        {
            string s = string.Empty;

            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
                client.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                byte[] resultBytes = client.DownloadData(url);

                if (client.ResponseHeaders["Content-Encoding"] == "gzip")
                {
                    using (MemoryStream stream = new MemoryStream(resultBytes))
                    {
                        using (GZipStream gzip = new GZipStream(stream, CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(gzip, System.Text.Encoding.Default))
                            {
                                s = reader.ReadToEnd();
                            }
                        }
                    }
                }
                else if (client.ResponseHeaders["Content-Encoding"] == "br")
                {
                    using (MemoryStream stream = new MemoryStream(resultBytes))
                    {
                        using (BrotliStream brotli = new BrotliStream(stream, CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(brotli, System.Text.Encoding.Default))
                            {
                                s = reader.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    s = System.Text.Encoding.Default.GetString(resultBytes);
                }
            }
            catch (Exception exp)
            {
                //s = new HttpClient().GetStringAsync(url).Result;
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(s);
            return doc;
        }

        public XmlDocument ToXml(string html)
        {
            return new XmlDocument();
        }

        public List<string> GetProp(XmlDocument xml, string path)
        {
            var result = new List<string>();
            return result;
        }
    }

    public class DefaultDateParser : IDateParser
    {
        public DateTime ParseDate(string input)
        {
            try
            {
                input = input.ToLower();

                if (input.Contains("aujourd'hui") || input.Contains("instant"))
                    return DateTime.Today;

                else if (input.Contains("il y a "))
                {
                    try
                    {
                        var index = int.Parse(Regex.Match(input, @"\d+").Value);
                        return DateTime.Today.AddDays(-index);
                    }
                    catch (Exception)
                    {
                        throw new SingleParseDateException($"could not parse date {input}");
                    }
                }

                else
                {
                    return DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception)
            {
                throw new SingleParseDateException($"could not parse date {input}");
            }
        }
    }

    public static class DateParserFactory
    {
        public static IDateParser CreateDateParser(DateParserType? parserType)
        {
            switch (parserType)
            {
                case DateParserType.DateParserType0:
                    return new DefaultDateParser();

                default:
                    return new DefaultDateParser();
            }
        }
    }
}
