using CommandSims.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Stories
{
    public class SomeoneBorned
    {
        public SomeoneBorned() { }

        public void PlayerBorn()
        {
            new WorldGenerator().CreateNewWorld(0);
            var msg = string.Format("{0},{1},你出生了", WorldGenerator.WorldTime, WorldGenerator.Weather);
            UI.PrintLine(msg);
            UI.PrintLine("");
            // 1
            // 3
            // 6
            // 10
            // 14
            // 16

        }

    }
}
