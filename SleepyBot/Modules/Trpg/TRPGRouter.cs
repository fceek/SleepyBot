using Maila.Cocoa.Framework;
using SleepyBot.Utilities;

namespace SleepyBot.Modules.Trpg
{
    [BotModule]
    public class TrpgRouter : BotModuleBase
    {
        

        [RegexRoute("(R|r)oll (?<rawCount>[0-9]+)(D|d)(?<rawDice>[0-9]+)")]
        public static string RollDice(MessageSource src, string rawCount, string rawDice)
        {
            Console.WriteLine("[Command Process]:RollDice");

            int count = int.Parse(rawCount);
            int dice = int.Parse(rawDice);

            if (count < 1)
            {
                return "必须至少掷出1个骰子！";
            }

            if (count > 50)
            {
                return "扔出的骰子太多了！";
            }
            if (dice < 2)
            {
                return "骰子至少要有2个面！";
            }

            string msg = string.Empty;
            if (src.IsGroup) msg = $"{src.MemberCard}掷出了{count}个{dice}面骰: \n";

            int total = 0;
            int current = 0;
            for (int i = 0; i < count - 1; i++)
            {
                current = Rng.Dice(dice);
                total += current;
                msg += $"{current}, ";
            }
            current = Rng.Dice(dice);
            total += current;

            msg += $"{current}.\n";
            msg += $"一共{total}点.";

            return msg;
        }
    }
}
