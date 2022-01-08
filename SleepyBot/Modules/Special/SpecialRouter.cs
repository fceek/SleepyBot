using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Framework;
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
    }
}
