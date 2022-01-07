using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Framework;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.Special
{
    [BotModule]
    public class SpecialRouter : BotModuleBase
    {
        [TextRoute("hexo s")]
        public static string GetLocalBlogLink(MessageSource src)
        {
            return "本地Hexo项目的默认地址：\nhttp://localhost:4000";
        }

        [TextRoute("hexo d"), DisableInPrivate]
        public static string GetDeployBlogLink(MessageSource src)
        {
            string blogLink = Data.GetBlogLink(src.User.Id);
            if (string.IsNullOrEmpty(blogLink))
            {
                return $"{src.MemberCard}，你还没有记录在案的博客。";
            }
            return $"{src.MemberCard}的博客已部署于{blogLink}";
        }
    }
}
