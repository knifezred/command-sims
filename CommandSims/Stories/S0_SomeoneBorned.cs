using CommandSims.Core;
using CommandSims.Enums;
using CommandSims.Modules.PokerCards;
using CommandSims.Modules.Seeds;
using CommandSims.Utils;
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
            Sims.World.CreateNewWorld(0);
            UI.ShowGradeColor();
            AnsiConsole.Write(new Rule("[red]序章[/]"));
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
                Name = name,
                Gender = gender,
                Race = race,
                Age = 0,
            };
            UI.LoadEvent("天赋选择");
            Sims.World.UpdateWorldTime(365);
            ChildhoodEvents();

            UI.ShowPlayerInfo();
            //new S1_BlackHouse().WakeUp();
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

        public void ChildhoodEvents()
        {
            AnsiConsole.Write(new Rule("[red]童年[/]"));
            // 出生前 自定义性别姓名种族
            var msg = string.Format("{0},{1},你出生了，是个{2}孩", Sims.World.GetWorldTime(), Sims.Weather.Value, Sims.Context.Player.Gender.GetEnumDisplayName());
            UI.PrintLine(msg);
            UI.LoadEvent("家境");

            UI.LoadEvent("抓周");


        }

        /// <summary>
        /// 打扑克
        /// </summary>
        public void PokerGame()
        {
            var pokerEngine = new PokerEngine();
            pokerEngine.PlayGame();
        }

    }
}
