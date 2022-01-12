using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Framework;
using SleepyBot.Structures;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.Test
{
    /// <summary>
    /// 一些测试指令，需要开发者权限。
    /// </summary>
    [BotModule]
    [IdentityRequirements(UserIdentity.Developer)]
    public class TestRouter : BotModuleBase
    {
        [TextRoute("Test:DBConnection")]
        public static string TestDBConnection(MessageSource src)
        {
            Console.WriteLine("Test:DBConnection");
            if (Data.TestDBConnection()) return "已连接至云端数据库。";
            return "云端数据库连接失败";
        }

        [RegexRoute("Test:GCoreCrawler PageRange=(?<rawPageNum>[1-9][0-9]?)")]
        public static void TestGCoreCrawler(MessageSource src, string rawPageNum)
        {
            src.Send("This may take a while...");
            Console.WriteLine("Test:GCoreCrawler PageRange=" + rawPageNum);
            int pageNum = int.Parse(rawPageNum);
            List<string> links = WebHtml.FilterGCoreRadioLinksTillPage(pageNum);
            Data.UpdateLinksFromSite("GCoreRadio", links);
            src.Send("finished");
        }

        [TextRoute("Test:GCoreCrawler CacheInfo")]
        public static void CachePageInfo(MessageSource src)
        {
            src.Send("This may take a while..."); 
            List<string> links = Data.GetAllLinksFromSite("GCoreRadio");
            int count = 0;
            foreach (string link in links)
            {
                Data.StoreGCoreLinkInfo(link);
                Console.WriteLine($"{++count}/{links.Count}");
            }
        }

        [TextRoute("Test:YabiAPI")]
        public static async void TestYabiAPI(MessageSource src)
        {
            var response = await WebHtml.GetYabiEvents(0, 10);
            foreach (WebResponses.YabiEvent yabiEvent in response)
            {
                Console.WriteLine(yabiEvent.name);
                Console.WriteLine(yabiEvent.timeStr);
            }
        }
    }
}
