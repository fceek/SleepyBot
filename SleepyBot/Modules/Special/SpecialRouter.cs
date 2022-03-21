using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Maila.Cocoa.Beans.Models.Messages;
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

        [RegexRoute("(.*)亚逼日历(!|！)(?<rawOption>.*(!|！))?")]
        public static async void GetYabiEvents(MessageSource src, string rawOption)
        {
            int count = 5;
            bool optionNoDescription = false; // 不要介绍
            bool optionShortDescription = false; // 短点介绍
            bool optionOnlyNearestOne = false; // 最近
            bool customNumber = false; // [1-6]条
            if (!string.IsNullOrEmpty(rawOption))
            {
                if (rawOption.Contains("不要介绍")) optionNoDescription = true;
                if (rawOption.Contains("短点介绍")) optionShortDescription = true;
                if (rawOption.Contains("最近")) optionOnlyNearestOne = true;
                if (rawOption.Contains("条"))
                {
                    char rawOptionNumber = rawOption[rawOption.IndexOf("条") - 1];
                    int optionNumber = (int)char.GetNumericValue(rawOptionNumber);
                    if (optionNumber > 6 || optionNumber <= 0)
                    {
                        src.Send("无效条数，默认显示5条");
                    }
                    else
                    {
                        count = optionNumber;
                    }
                }
            }

            if (optionOnlyNearestOne) count = 1;
            int descLength = optionShortDescription ? 43 : 83;
            var events = await WebHtml.GetYabiEvents(0, count);
            string response = string.Empty;
            foreach (WebResponses.YabiEvent yabiEvent in events)
            {
                if (optionNoDescription)
                {
                    response += $"{yabiEvent.name}\n{yabiEvent.timeStr}\n\n";
                    continue;
                }
                string desc = yabiEvent.description.Replace("\n", "/").TrimEnd('/');
                if (desc.Length > descLength)
                {
                    desc = desc.Substring(0, descLength - 3) + "...";
                }
                response += $"{yabiEvent.name}\n{yabiEvent.timeStr}\n{desc}\n\n";
            }
            await src.SendAsync(response);
        }

        [RegexRoute("(.*)(为什么|Why|why|WHY)(.*)")]
        public static void WhyRepeater(MessageSource src, QMessage msg)
        {
            src.Send(msg + "！");
        }

        [RegexRoute("^(H|h)elp!")]
        public static void HelpInfo(MessageSource src)
        {
            src.Send("文档从这里看起 https://fceek.github.io/wiki/SleepyBot/Commands/TRPG.html");
        }
    }
}
