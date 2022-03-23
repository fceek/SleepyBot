using Maila.Cocoa.Framework;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.Trpg
{
    /// <summary>
    /// Trpg相关的指令。
    /// </summary>
    [BotModule]
    public class TrpgRouter : BotModuleBase
    {

        // [RegexRoute("(R|r)oll (?<rawCount>[0-9]+)(D|d)(?<rawDice>[0-9]+)")]
        // public static string RollDice(MessageSource src, string rawCount, string rawDice)
        // {
        //     Console.WriteLine("[Command Process]:RollDice");
        //
        //     int count = int.Parse(rawCount);
        //     int dice = int.Parse(rawDice);
        //
        //     if (count < 1)
        //     {
        //         return "必须至少掷出1个骰子！";
        //     }
        //
        //     if (count > 50)
        //     {
        //         return "扔出的骰子太多了！";
        //     }
        //     if (dice < 2)
        //     {
        //         return "骰子至少要有2个面！";
        //     }
        //
        //     string msg = string.Empty;
        //     if (src.IsGroup) msg = $"{src.MemberCard}掷出了{count}个{dice}面骰: \n";
        //
        //     int total = 0;
        //     int current = 0;
        //     for (int i = 0; i < count - 1; i++)
        //     {
        //         current = Rng.Dice(dice);
        //         total += current;
        //         msg += $"{current}, ";
        //     }
        //     current = Rng.Dice(dice);
        //     total += current;
        //
        //     msg += $"{current}.\n";
        //     msg += $"一共{total}点.";
        //
        //     return msg;
        // }

        [RegexRoute("(R|r)oll (?<body>(([0-9]+)(D|d|[+])?)+)")]
        public static string RollDiceEx(MessageSource src, string body)
        {
            string response = src.IsGroup?$"{src.MemberCard}" : string.Empty;
            int result = 0;
            
            body = body.ToLower();
            string[] diceGroups = new string[1];
            if (body.Contains('+'))
            {
                diceGroups = body.Split(new char[] {'+'});
            }
            else if (body.Contains('d'))
            {
                diceGroups[0] = body;
            }
            foreach (string diceGroup in diceGroups)
            {
                (string groupResponse, int groupResult) = ParseGroup(diceGroup);
                response += groupResponse;
                result += groupResult;
            }

            response += $"总计{result}点。";
            return response;
        }

        private static (string, int) ParseGroup(string diceGroup)
        {
            if (diceGroup.Contains('d'))
            {
                string[] groupCmd = diceGroup.Split(new char[] {'d'});
                int count = int.Parse(groupCmd[0]);
                int dice = int.Parse(groupCmd[1]);
                return RollSingle(count, dice);
            }
            else
            {
                return (string.Empty, int.Parse(diceGroup));
            }
        }

        private static (string, int) RollSingle(int count, int dice)
        {
            string response = string.Empty;
            int result = 0;
            if (count < 1)
            {
                return ("必须至少掷出1个骰子！", 0);
            }
            if (count > 50)
            {
                return ("扔出的骰子太多了！", 0);
            }
            if (dice < 2)
            {
                return ("骰子至少要有2个面！", 0);
            }

            response += $"掷出了{count}个{dice}面骰： ";
            int current = 0;
            for (int i = 0; i < count - 1; i++)
            {
                current = Rng.Dice(dice);
                result += current;
                response += $"{current}, ";
            }

            current = Rng.Dice(dice);
            result += current;
            response += $"{current}. ";
            response += $"共{result}点.\n";

            return (response, result);
        }
    }
}
