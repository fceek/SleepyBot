using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Framework;
using SleepyBot.Structures;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.Special
{
    /// <summary>
    /// 这里用来放一些没想好分类的指令。
    /// </summary>
    [BotModule]
    public class SpecialRouter : BotModuleBase
    {
        [TextRoute("hexo s")]
        public static string GetLocalBlogLink(MessageSource src)
        {
            return "本地Hexo项目的默认地址：\nhttp://localhost:4000";
        }

        [RegexRoute("(.*)亚逼日历(.*)(!|！)")]
        public static async void GetYabiEvents(MessageSource src)
        {
            var events = await WebHtml.GetYabiEvents(0, 5);
            string response = string.Empty;
            foreach (WebResponses.YabiEvent yabiEvent in events)
            {
                string desc = yabiEvent.description.Replace("\n", "/").TrimEnd('/');
                if (desc.Length > 83)
                {
                    desc = desc.Substring(0, 80) + "...";
                }
                response += $"{yabiEvent.name}\n{yabiEvent.timeStr}\n{desc}\n\n";
            }

            response += "=====\n5 Incoming Events";
            await src.SendAsync(response);
        }
    }
}
