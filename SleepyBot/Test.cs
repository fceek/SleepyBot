using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Beans.Models.Messages;
using Maila.Cocoa.Framework;

namespace SleepyBot
{
    [BotModule]
    public class Test : BotModuleBase
    {
        [TextRoute("我真的佛了")]
        public static void Run(MessageSource src)
        {
            src.Send("终于成了");
        }

        [TextRoute("roll")]
        public static void Roll(MessageSource src)
        {
            var random = new Random();
            src.Send(random.Next(0, 100).ToString());
        }

        [TextRoute("hexo s")]
        public static void AtMe(MessageSource src)
        {
            if (src.IsGroup && src.User.Id == 1844186110)
            {
                src.Send(new AtMessage(475017297));
            }
        }
    }
}
