using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepyBot.Utilities
{
    public static class Time
    {
        public static string FromNowToTargetStr(DateTime target)
        {
            TimeSpan span = target - DateTime.Now;
            string response = string.Empty;
            if (span.Days >= 1)
            {
                response += span.Days + "天";
            }

            response += $"{span.Hours}小时{span.Minutes}分{span.Seconds}秒";
            return response;
        }
    }
}
