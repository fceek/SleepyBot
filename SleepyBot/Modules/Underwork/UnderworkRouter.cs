using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Framework;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.Underwork
{
    [BotModule]
    public class UnderworkModule : BotModuleBase
    {
        [RegexRoute("(.*)下班(.*)(!|！)")]
        public static string GetTimeTillOffWork(MessageSource src)
        {
            DateTime offWorkTime = DateTime.Today.AddHours(19);
            if (DateTime.Compare(offWorkTime, DateTime.Now) < 0)
            {
                return "已经下班了！";
            }

            string response = Time.FromNowToTargetStr(offWorkTime);
            return $"离下班还有{response}！";
        }

        [RegexRoute("(.*)听听机核(.*)")]
        public static string GetRandomGcorePodcast(MessageSource src)
        {
             List<string> links = Data.GetAllLinksFromSite("GCoreRadio");
            if (links.Count == 0)
            {
                return "获取电台链接发生错误";
            }

            string linkNum = links.GetRandom();
            string link = $"https://www.gcores.com/radios/{linkNum}";
            string[] info = Data.StringGet($"Links:GCoreRadio:{linkNum}").Split('|');
            if (info.Length != 2)
            {
                return $"{link}\n详细信息缓存错误";
            }
            string title = info[0];
            string description = info[1];
            return $"{title}\n{description}\n{link}";
        }
    }
}
