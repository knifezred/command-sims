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
    /// 序章 创建角色，基础属性设置
    /// </summary>
    public class S0_SomeoneBorned
    {
        public void PlayerBorn()
        {
            AnsiConsole.Write(new Rule("[red]序章[/]"));
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
