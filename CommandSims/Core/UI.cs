using CommandSims.Enums;
using CommandSims.Stories;
using KnifeZ.Unity.Extensions;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Core
{
    public static class UI
    {

        #region Console.Write重写，支持颜色设置，打字机效果

        public static void Print(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Typewriter(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintLine(string message, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Typewriter(message);
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Typewriter(string message, int delay = 10)
        {
            var msgs = message.ToArray();
            foreach (var msg in msgs)
            {
                Console.Write($"{msg}");
                Thread.Sleep(delay);
            }

        }

        #endregion

        public static void LoadStartPanel()
        {
            AnsiConsole.Write(new FigletText("Command Sims").Centered().Color(Color.Blue));
            var result = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Welcome to [red]CommansSims[/]")
                .PageSize(10)
                .AddChoices(new string[]
                {
                    "新的开始","继续游戏"
                }));
            if (result == "继续游戏")
            {
                Sims.GameFramework.LoadArchive("");
            }
            if (result == "新的开始")
            {
                new S0_SomeoneBorned().PlayerBorn();
            }
        }

        #region Spectre.Console

        public static RaceEnum ChooseRace()
        {
            var raceList = EnumExtension.ToListItems(typeof(RaceEnum));
            if (raceList.Any())
            {
                var items = raceList.Select(x => x.Text.ToString().ToString()).ToArray();
                var result = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("What's your [green]race[/]?")
                    .PageSize(10)
                    .AddChoices(items));
                AnsiConsole.MarkupLine($"What's your [green]race[/]? you choose {result} !");
                return EnumExtension.GetValueFromName<RaceEnum>(result);
            }
            return RaceEnum.Human;
        }

        public static GenderEnum ChooseGender()
        {
            var enums = EnumExtension.ToListItems(typeof(GenderEnum));
            if (enums.Any())
            {
                var items = enums.Select(x => x.Text.ToString().ToString()).ToArray();
                var result = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("What's your [green]gender[/]?")
                    .PageSize(10)
                    .AddChoices(items));
                AnsiConsole.MarkupLine($"What's your [green]gender[/]? you are {result} !");
                return EnumExtension.GetValueFromName<GenderEnum>(result);
            }
            return GenderEnum.Other;
        }

        #endregion

        public static void ShowPalyerInfo()
        {
            PrintLine("------------------", ConsoleColor.Green);
            PrintLine("姓名: " + Sims.PlayerData.PlayerInfo.Name, ConsoleColor.Cyan);
            PrintLine("------------------", ConsoleColor.Green);
        }


        /// <summary>
        /// 显示角色状态
        /// </summary>
        public static void ShowNpcStatus(string npcName)
        {
            var info = Sims.NpcList.FirstOrDefault(x => x.Name == npcName);
            if (info != null)
            {
                PrintLine("------------------", ConsoleColor.Green);
                PrintLine("姓名: " + info.Name, ConsoleColor.Cyan);
                PrintLine("------------------", ConsoleColor.Green);
            }
        }
    }
}
