
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Common
{
    public abstract class SearchSetting
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public bool IsDesc { get; set; }
        public string OrderColumn { get; set; }

        public SearchSetting()
        {
            PageSize = 5;
            PageIndex = 0;
        }

        public string GetWhereLine()
        {
            var props = GetType().GetProperties().Where(x => 
                x.GetCustomAttributes<SearchFilterAttribut>().Any()
                && x.GetValue(this, null) != null
            ).ToList();

            List<string> whereLines = new List<string>();

            if (props.Count == 0)
                return true.ToString();
            else
            {
                foreach (var prop in props)
                {
                    string propWhereLine = string.Empty;
                    string value = prop.GetValue(this, null).ToString();
                    string sqlPromName = prop.Name.ToLower().Replace("min", "").Replace("max", "").Replace("'", "''");

                    Comparator comparator = prop.GetCustomAttribute<SearchFilterAttribut>().GetComparator();
                    if (comparator != Comparator.like)
                    {
                        if (new List<Type>() { typeof(string), typeof(char), typeof(DateTime) }.Contains(prop.PropertyType))
                            value = "'" + value + "'";
                        string comparatorDescription = GetDescription(comparator);
                        whereLines.Add($"{sqlPromName} {comparatorDescription} {value}");
                    }
                    else
                    {
                        whereLines.Add($"{sqlPromName}.Contains(\"{value}\")");
                    }
                }

                return whereLines.Aggregate((x, y) => x + " and " + y);
            }
        }

        private static string GetDescription(Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }
    }

    public enum Comparator
    {
        [Description("=")] eq,
        like,
        [Description(">")] lt,
        [Description(">=")] lte,
        [Description("<")] gt,
        [Description("<=")] gte,
    }

    public class SearchFilterAttribut : Attribute
    {
        private readonly Comparator _comparator;
        private readonly string _sqlFieldName;

        public SearchFilterAttribut(Comparator comparator, string SqlFieldName = "")
        {
            _sqlFieldName = SqlFieldName;
            _comparator = comparator;
        }

        public Comparator GetComparator() { return _comparator; }
    }
}
