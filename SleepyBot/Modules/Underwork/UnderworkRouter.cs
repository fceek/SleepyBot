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
        [RegexRoute("(.*)过年放假(.*)")]
        public static string GetTimeTillNewYear(MessageSource src)
        {
            string response = Time.FromNowToTargetStr(new DateTime(2022, 1, 26, 19, 0, 0, DateTimeKind.Local));
            return $"离过年放假还有{response}！";
        }

        [RegexRoute("(.*)下班(.*)")]
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
    }
}
