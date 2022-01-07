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
    Console.WriteLine("Startup OK");

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