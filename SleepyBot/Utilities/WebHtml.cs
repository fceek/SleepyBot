using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlAgilityPack;
using SleepyBot.Structures;

namespace SleepyBot.Utilities
{
    public static class WebHtml
    {
        private static readonly HtmlWeb Parser = new();
        private static HttpClient client;

        private static JsonSerializerOptions options = new()
        {
            IncludeFields = true
        };

        private static bool _httpClientInited = false;
        private static void InitHttpClient()
        {
            HttpClientHandler handler = new();
            handler.ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true;
            client = new HttpClient(handler);
            _httpClientInited = true;
        }

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

        public static async Task<List<WebResponses.YabiEvent>> GetYabiEvents(int from, int count)
        {
            if (!_httpClientInited) InitHttpClient();
            string request = $"https://api.fizzli.dev/event/latest?from={from}&limit={count}";
            var response = await client.GetStringAsync(request);
            WebResponses.YabiData data = JsonSerializer.Deserialize<WebResponses.YabiData>(response, options);

            Console.WriteLine(data);
            return data.data;
        }
    }
}
