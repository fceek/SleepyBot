using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Framework;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.NoteBook
{
    [BotModule]
    public class NoteBookRouter : BotModuleBase
    {
        [RegexRoute("提交(:|：)博客(.*)(?<rawAddr>(H|h)ttp.*)")]
        public static string SubmitBlog(MessageSource src, string rawAddr)
        {
            src.Send($"尝试提交{src.User.Id}的博客链接");
            bool success = Data.SetBlogLink(src.User.Id, rawAddr);
            if (success) return "数据已提交至云端数据库";
            return "数据提交失败";
        }
    }
}
