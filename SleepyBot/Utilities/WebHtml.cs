using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SleepyBot.Utilities
{
    public static class WebHtml
    {
        private static readonly HtmlWeb Parser = new ();

        public static string GetNodeFromLink(string link, string node)
        {
            var htmlDoc = Parser.Load(link);
            if (htmlDoc != null)
            {
                var targetNode = htmlDoc.DocumentNode.SelectSingleNode(node);
                if (targetNode != null)
                {
                    return targetNode.InnerHtml;
                }
            }
            
            return string.Empty;
        }

        public static List<string> FilterGCoreRadioLinks(int page)
        {
            List<string> links = new();
            string listPage = $"https://www.gcores.com/radios?page={page}";
            var htmlDoc = Parser.Load(listPage);
            if (htmlDoc != null)
            {
                var targetNodes = htmlDoc.DocumentNode.SelectNodes("//a[@class=\"am_card_content original_content\"]");
                foreach (var targetNode in targetNodes)
                {
                    string link = targetNode.GetAttributeValue("href", string.Empty);
                    if (link.Contains("radios/"))links.Add(link.Replace("/radios/",""));
                }
            }

            return links;
        }

        public static List<string> FilterGCoreRadioLinksTillPage(int page)
        {
            List<string> links = new();
            for (int i = 1; i <= page; i++)
            {
                links.AddRange(FilterGCoreRadioLinks(i));
            }

            return links;
        }
    }
}
