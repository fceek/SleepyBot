using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepyBot.Structures
{
    public class WebResponses
    {
        public struct YabiData
        {
            public List<YabiEvent> data;
        }

        public struct YabiEvent
        {
            public string name;
            public string description;
            public string timeStr;
        }
    }
}
