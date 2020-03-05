using HtmlAgilityPack;
using jh.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml;

namespace jh
{
    public class Helper : IHelper
    {
        public HtmlDocument Download(string url)
        {
            string s = string.Empty;
            WebClient client = new WebClient();
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);

            try
            {
                client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                s = reader.ReadToEnd();
            }
            catch (Exception)
            {
                s = new HttpClient().GetStringAsync(url).Result;
            }
            finally 
            { 
                data.Close();
                reader.Close();            
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
                    throw new Exception();
                }
            }

            else
            {
                return DateTime.ParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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
