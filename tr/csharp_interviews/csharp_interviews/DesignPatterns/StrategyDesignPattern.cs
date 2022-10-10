using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.DesignPatterns
{
    internal class StrategyDesignPattern
    {
        public static void MainMethod() 
        {
            var tp = new TextProcessor();
            tp.SetOutputFormat(OutputFormat.Markdown);
            tp.AppendList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(tp);

            tp.CLear();
            tp.SetOutputFormat(OutputFormat.Html);
            tp.AppendList(new[] { "foo", "bar", "baz" });
            Console.WriteLine(tp);
        }

        public enum OutputFormat 
        { 
            Markdown,
            Html
        }

        public interface IListStrategy
        {
            void Start(StringBuilder sb);
            void End(StringBuilder sb);
            void AddListItem(StringBuilder sb, string item);
        }

        public class HtmlListStrategy : IListStrategy
        {
            public void AddListItem(StringBuilder sb, string item)
            {
                sb.Append($" <li>{item}</li>");
            }

            public void End(StringBuilder sb)
            {
                sb.Append("<ul>");
            }

            public void Start(StringBuilder sb)
            {
                sb.Append("<ul>");
            }
        }

        public class MarkedDownListStrategy : IListStrategy
        {
            public void AddListItem(StringBuilder sb, string item)
            {
                sb.Append($" * {item}");
            }

            public void End(StringBuilder sb)
            {
            }

            public void Start(StringBuilder sb)
            {
            }
        }

        public class TextProcessor 
        {
            private StringBuilder sb = new StringBuilder();
            private IListStrategy listStrategy;

            public void SetOutputFormat(OutputFormat outputFormat) 
            {
                switch (outputFormat)
                {
                    case OutputFormat.Markdown:
                        listStrategy = new MarkedDownListStrategy();
                        break;
                    case OutputFormat.Html:
                        listStrategy = new HtmlListStrategy();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(outputFormat), outputFormat, null);
                }
            }

            public void AppendList(IEnumerable<string> items) 
            {
                listStrategy.Start(sb);
                foreach (string item in items) 
                {
                    listStrategy.AddListItem(sb, item);
                }
            }

            public StringBuilder CLear() => sb.Clear();

            public override string ToString()
            {
                return sb.ToString();
            }
        }
    }
}
