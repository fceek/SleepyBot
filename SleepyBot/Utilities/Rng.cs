using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepyBot.Utilities
{
    public static class Rng
    {
        private static Random? _random;

        private static Random LazyRng
        {
            get
            {
                if (_random == null) _random = new Random();
                return _random;
            }
        }

        public static int Dice(int faceCount)
        {
            return LazyRng.Next(1, faceCount + 1);
        }
    }
}
