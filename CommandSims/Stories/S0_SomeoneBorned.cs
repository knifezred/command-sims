using CommandSims.Core;
using CommandSims.Modules.Seeds;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            UI.PrintLine("");
            var name = AnsiConsole.Prompt(new SelectionPrompt<string>()
                   .Title("名字")
                   .PageSize(10)
                   .AddChoices(new string[] { "1. 随机", "2. 自定义" }));
            if (name.StartsWith("1."))
            {
                name = ReRandomName();
            }
            else
            {
                name = AnsiConsole.Ask<string>("请输入名字:");
            }
            var gender = UI.ChooseGender();
            var race = UI.ChooseRace();
            Sims.PlayerData.PlayerInfo = new Entity.Npc.Player
            {
                Id = 0,
                Exp = 0,
                MaxHP = 100,
                MaxMP = 100,
                Speed = 10,
                Name = name,
                Gender = gender,
                Race = race
            };
            WorldFramework.WorldTime.AddYears(1);
            Sims.PlayerData.PlayerInfo.Age = 1;
            var babySeize = AnsiConsole.Prompt(new SelectionPrompt<string>()
                   .Title($"{WorldFramework.WorldTime:yyyy年MM月dd},抓周")
                   .PageSize(10)
                   .AddChoices(new string[] { "1. 一把小刀", "2. 算盘", "3. 道书", "4. 佛经", "5. 剪刀" }));
            AnsiConsole.MarkupLine($"1岁抓周时，你选择了[green]{babySeize.Split('.')[1]}[/]");
            // todo 添加用户属性

            // 1
            // 3
            // 6
            // 10
            // 14
            // 16
            UI.ShowPlayerInfo();

        }

        public string ReRandomName()
        {
            var name = new SeedsData().GetRandomFullName();
            UI.PrintLine(name);
            var reName = AnsiConsole.Prompt(new SelectionPrompt<string>()
               .Title("继续随机")
               .PageSize(10)
               .AddChoices(new string[] { "1. 随机", "2. 使用当前名字" }));
            if (reName.StartsWith("1."))
            {
                name = ReRandomName();
            }
            return name;
        }

    }
}
