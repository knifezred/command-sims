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
            new WorldFramework().CreateNewWorld(0);
            AnsiConsole.Write(new Rule("[red]序章[/]"));
            var msg = string.Format("{0},{1},你出生了", WorldFramework.WorldTime, WorldFramework.Weather);
            UI.PrintLine(msg);
            var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
            var gender = UI.ChooseGender();
            var race = UI.ChooseRace();
            Sims.PlayerData.PlayerInfo.Name = name;
            Sims.PlayerData.PlayerInfo.Gender = gender;
            Sims.PlayerData.PlayerInfo.Race = race;

            WorldFramework.WorldTime.AddYears(1);
            Sims.PlayerData.PlayerInfo.Age = 1;
            var babySeize = AnsiConsole.Prompt(new SelectionPrompt<string>()
                   .Title($"{WorldFramework.WorldTime:yyyy年MM月dd},抓周")
                   .PageSize(10)
                   .AddChoices(new string[] { "1. 一把小刀", "2. 算盘", "3. 道书", "4. 佛经", "5. 剪刀" }));
            AnsiConsole.MarkupLine($"1岁抓周时，你选择了{babySeize}");
            // todo 添加用户属性

            // 1
            // 3
            // 6
            // 10
            // 14
            // 16

        }

    }
}
