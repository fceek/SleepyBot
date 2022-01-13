using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Framework;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.Database
{
    [BotModule, DisableInPrivate]
    public class LookUpRouter : BotModuleBase
    {
        [TextRoute("hexo d")]
        public static string GetDeployBlogLink(MessageSource src)
        {
            string blogLink = Data.GetBlogLink(src.User.Id);
            if (string.IsNullOrEmpty(blogLink))
            {
                return $"{src.MemberCard}，你还没有记录在案的博客。";
            }
            return $"{src.MemberCard}的博客已部署于{blogLink}";
        }

        [RegexRoute("(.*)吃什么(！|!)")]
        public static void GetRandomRestaurant(MessageSource src)
        {
            string response = Data.GetRandomRestaurant(src.Group.Id);
            src.Send(response);
        }

        [RegexRoute("(.*)有什么吃的(!|！)?")]
        public static void GetAllRestaurant(MessageSource src)
        {
            src.Send("我去看看！");
            string response = Data.GetAllRestaurant(src.Group.Id);
            src.Send(response);
        }

        [RegexRoute("(.*)答案之书(.*)(!|！)")]
        public static void GetRandomBoa(MessageSource src)
        {
            src.Send("它说： " + Data.GetRandomBoa() + "\n答案之书获取自 https://github.com/D1N910/answers-of-my-life");
        }
    }
}
