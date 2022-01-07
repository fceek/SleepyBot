using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maila.Cocoa.Framework;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.Test
{
    [BotModule]
    [IdentityRequirements(UserIdentity.Developer)]
    public class TestRouter : BotModuleBase
    {
        [TextRoute("Test:DBConnection")]
        public static string TestDBConnection(MessageSource src)
        {
            Console.WriteLine("Test:DBConnection");
            if (Data.TestDBConnection()) return "已连接至云端数据库。";
            return "云端数据库连接失败";
        }
    }
}
