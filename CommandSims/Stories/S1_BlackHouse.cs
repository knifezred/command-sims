using CommandSims.Core;
using CommandSims.Enums;
using CommandSims.Utils;
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

        public void Start()
        {

        }

        /// <summary>
        /// 小黑屋-醒来
        /// </summary>
        public void WakeUp()
        {
            AnsiConsole.Write(new Rule("[blue]醒来[/]"));
            UI.ShowMoveMap(2);
            UI.PrintLine("");
        }


    }
}
