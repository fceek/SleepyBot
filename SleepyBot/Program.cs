using System.Configuration;
using System.Collections.Specialized;
using System;
using Maila.Cocoa.Beans.API;
using Maila.Cocoa.Framework;
using Maila.Cocoa.Framework.Support;
using SleepyBot;

BotStartupConfig config = new(Secret.VerifyKey, Secret.BotId, Secret.MiraiHost, Secret.MiraiPort);
bool succeed = await BotStartup.Connect(config);

if (succeed)
{
    Console.WriteLine("Bot Backend connection OK!");

    // Do module init. e.g. prefetch data from database todo 

    BotAPI.OnBotInvitedJoinGroupRequest = e =>
    {
        if (e.FromId == Secret.DevId)
        {
            e.Response(
                BotInvitedJoinGroupRequestOperate.Agree);
        }
    };

    // Do auth. e.g. add myself as developer

    BotAuth.SetIdentity(Secret.DevId, UserIdentity.Developer);

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
    internal static partial class Secret
    {
        // public const string VerifyKey = "";
        // public const long BotId = ;
        // public const string MiraiHost = "";
        // public const int MiraiPort = ;
        // public const string RedisEndpoint = "";
        // public const string RedisAuthKey = "";
        // public const long DevId = ;
    }
}