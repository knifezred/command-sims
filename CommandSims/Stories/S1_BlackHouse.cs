using CommandSims.Core;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Stories
{
    /// <summary>
    /// 小黑屋
    /// tips: 找到钥匙并离开小黑屋
    /// </summary>
    public class S1_BlackHouse
    {
        /// <summary>
        /// 小黑屋-醒来
        /// </summary>
        public void WakeUp()
        {
            AnsiConsole.Write(new Rule("[blue]醒来[/]"));
            UI.PrintLine("");
        }


    }
}
