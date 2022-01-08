using System.Configuration;
using System.Collections.Specialized;
using System;
using Maila.Cocoa.Framework;
using Maila.Cocoa.Framework.Support;
using SleepyBot;

BotStartupConfig config = new(Secret.V_KEY, Secret.QQ, Secret.HOST, Secret.PORT);
var succeed = await BotStartup.Connect(config);

if (succeed)
{
    Console.WriteLine("Bot Backend connection OK!");

    // Do module init. e.g. prefetch data from database todo 

    

    // Do auth. e.g. add myself as developer

    BotAuth.SetIdentity(Secret.DEV_QQ, UserIdentity.Developer);

    while (Console.ReadLine() != "exit")
    {
    }
    await BotStartup.Disconnect();
}
else
{
    Console.WriteLine("Failed");
}


// 拉到新环境之后新建一个Secret.cs，把下面的内容复制过去填好即配置完成。
// Remote: Mirai console with mirai-http-api, Redis.
namespace SleepyBot
{
    static partial class Secret
    {
        //public static string V_KEY = "";
        //public static long QQ = ;
        //public static string HOST = "";
        //public static int PORT = ;
        //public static string REDIS_ENDPOINT = "";
        //public static string REDIS_AUTHKEY = "";
        //public static long DEV_QQ = ;
    }
}