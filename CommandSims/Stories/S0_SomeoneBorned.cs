﻿using CommandSims.Core;
using CommandSims.Enums;
using CommandSims.Modules.PokerCards;
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
            var pokerEngine = new PokerEngine();
            pokerEngine.PlayGame();


            Sims.World.CreateNewWorld(0);
            AnsiConsole.Write(new Rule("[red]序章[/]"));
            var msg = string.Format("{0},{1},你出生了", Sims.World.GetWorldTime(), Sims.Weather.Value);
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
            var race = UI.EnumSelect<RaceEnum>();
            var gender = UI.EnumSelect<GenderEnum>();
            Sims.Context.Player = new Entity.Npc.Player
            {
                Id = 0,
                Exp = 0,
                HP = 100,
                MP = 100,
                Speed = 10,
                Name = name,
                Gender = gender,
                Race = race
            };
            Sims.World.UpdateWorldTime(365);
            Sims.Context.Player.Age = 1;
            var babySeize = AnsiConsole.Prompt(new SelectionPrompt<string>()
                   .Title($"{Sims.WorldTime:yyyy年MM月dd},抓周")
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
            new S1_BlackHouse().WakeUp();
        }

        public string ReRandomName()
        {
            var name = Sims.Seeds.GetRandomFullName();
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
