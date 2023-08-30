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
        private static bool Busy = false;
        #region UI占用检测

        public static bool IsBusy()
        {
            return Busy;
        }

        public static void Running()
        {
            Busy = true;
        }

        public static void SetFree()
        {
            Busy = false;
        }
        #endregion

        #region Console.Write重写，支持颜色设置，打字机效果

        public static void Print(string message, ConsoleColor color = ConsoleColor.Green)
        {
            UI.Running();
            Console.ForegroundColor = color;
            Typewriter(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintLine(string message, ConsoleColor color = ConsoleColor.Green)
        {
            UI.Running();
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
            UI.Running();
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
                Sims.Game.LoadArchive("");
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

        /// <summary>
        /// 帮助信息
        /// </summary>
        /// <param name="commands"></param>
        public static void HelpInfo(string[] commands)
        {
            UI.PrintLine("特殊命令：load/读档 [存档名字] save/存档 [存档名字] exit/退出", ConsoleColor.Magenta);
            UI.PrintLine("通用命令：操作 目标 [数量] [使用对象]", ConsoleColor.Blue);
            UI.PrintLine("操作：open/o/打开 ", ConsoleColor.Green);
            UI.PrintLine("示例 打开背包(下同不再列举)：1. open bag    2. o bag    3. 打开 bag    4. 打开 背包", ConsoleColor.DarkGray);
            UI.PrintLine("操作：use/u/用", ConsoleColor.Green);
            UI.PrintLine("示例 使用物品：use 荷包蛋 1", ConsoleColor.DarkGray);
            UI.PrintLine("操作：give/g/送", ConsoleColor.Green);
            UI.PrintLine("示例 赠送物品：送 荷包蛋 1 张三", ConsoleColor.DarkGray);
            UI.PrintLine("操作：drop/d/扔", ConsoleColor.Green);
            UI.PrintLine("示例 丢弃物品：drop 荷包蛋 1", ConsoleColor.DarkGray);
            UI.PrintLine("操作：trade/t/交易", ConsoleColor.Green);
            UI.PrintLine("示例 交易物品：trade 荷包蛋 1 张三", ConsoleColor.DarkGray);
            UI.PrintLine("操作：learn/学习", ConsoleColor.Green);
            UI.PrintLine("示例 学习技能：learn 清风剑谱", ConsoleColor.DarkGray);
            UI.PrintLine("操作：make/m/制作", ConsoleColor.Green);
            UI.PrintLine("示例 制作物品：make 桃木剑 1", ConsoleColor.DarkGray);
            UI.PrintLine("操作：destroy/毁", ConsoleColor.Green);
            UI.PrintLine("示例 摧毁物品：destroy 桃木剑 1", ConsoleColor.DarkGray);
        }
    }
}
