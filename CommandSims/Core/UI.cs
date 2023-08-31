using CommandSims.Enums;
using CommandSims.Modules.Maps;
using CommandSims.Stories;
using CommandSims.Utils;
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
        #region UI占用检测

        private static bool Busy = false;

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

        #region 地图

        public static void ShowMap(int mapId)
        {
            var maps = Sims.World.Map.GetArroundMaps(mapId).OrderBy(x => x.LocationX).ThenByDescending(x => x.LocationY).ToList();
            Sims.Context.CurrentMap = maps.First(x => x.Id == mapId);
            List<MoveDirection> moveDirections = new()
            {
                MoveDirection.Center
            };
            if (Sims.World.Map.CanEnter(Sims.Context.CurrentMap.Id))
            {
                moveDirections.Add(MoveDirection.Enter);
            }
            if (Sims.Context.CurrentMap.CanOut)
            {
                moveDirections.Add(MoveDirection.Exit);
            }
            var table = new Table();
            var tempX = Sims.Context.CurrentMap.LocationX;
            var tempY = Sims.Context.CurrentMap.LocationY;
            string[] rows1 = new string[3] { "", "", "" };
            string[] rows2 = new string[3] { "", "", "" };
            string[] rows3 = new string[3] { "", "", "" };
            table.AddColumn("西");
            table.AddColumn("");
            table.AddColumn("东");
            foreach (var map in maps)
            {
                if (map.LocationX < tempX && map.LocationY == tempY)
                {
                    moveDirections.Add(MoveDirection.Left);
                    rows2[0] = map.Name;
                }
                if (map.LocationX == tempX && map.LocationY == tempY)
                {
                    rows2[1] = map.Name;
                }
                if (map.LocationX > tempX && map.LocationY == tempY)
                {
                    moveDirections.Add(MoveDirection.Right);
                    rows2[2] = map.Name;
                }

                if (map.LocationX < tempX && map.LocationY > tempY)
                {
                    moveDirections.Add(MoveDirection.LeftUp);
                    rows1[0] = map.Name;
                }
                if (map.LocationX == tempX && map.LocationY > tempY)
                {
                    moveDirections.Add(MoveDirection.Up);
                    rows1[1] = map.Name;
                }
                if (map.LocationX > tempX && map.LocationY > tempY)
                {
                    moveDirections.Add(MoveDirection.RightUp);
                    rows1[2] = map.Name;
                }

                if (map.LocationX < tempX && map.LocationY < tempY)
                {
                    moveDirections.Add(MoveDirection.LeftDown);
                    rows3[0] = map.Name;
                }
                if (map.LocationX == tempX && map.LocationY < tempY)
                {
                    moveDirections.Add(MoveDirection.Down);
                    rows3[1] = map.Name;
                }
                if (map.LocationX > tempX && map.LocationY < tempY)
                {
                    moveDirections.Add(MoveDirection.RightDown);
                    rows3[2] = map.Name;
                }

            }
            table.AddRow(rows1);
            table.AddRow(rows2);
            table.AddRow(rows3);
            table.Centered();
            AnsiConsole.Write(table);
            ShowMoveDirection(moveDirections);
        }

        public static void ShowMoveDirection(List<MoveDirection> moveDirections)
        {
            var actions = moveDirections.Select(x => x.GetEnumDisplayName()).ToArray();
            var result = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[green]你可进行如下操作[/]")
                .PageSize(10)
                .AddChoices(actions));
            var direction = EnumExtension.GetValueFromName<MoveDirection>(result);
            Sims.Game.MapMove(direction);
            UI.PrintLine(Sims.Context.CurrentMap.Description);
            if (direction != MoveDirection.Center)
            {
                // 除非选择留在此处，否则继续触发移动选项
                ShowMap(Sims.Context.CurrentMap.Id);
            }
        }

        #endregion

        /// <summary>
        /// 开始面板
        /// </summary>
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
                //new S0_SomeoneBorned().PlayerBorn();
                new S1_BlackHouse().WakeUp();
            }
        }

        /// <summary>
        /// 显示角色信息
        /// </summary>
        /// <param name="playerId"></param>
        public static void ShowPlayerInfo(int playerId = 0)
        {
            var player = Sims.GetPlayer(playerId);
            if (player != null)
            {
                PrintLine("------------------", ConsoleColor.Green);
                PrintLine("姓名: " + player.Name, ConsoleColor.Cyan);
                PrintLine("------------------", ConsoleColor.Green);
            }

        }

        public static void ShowMapInfo(int mapId = 0)
        {
            var map = Sims.World.Map.GetMapById(mapId);
            UI.PrintLine(map.Description);
            ShowRoomNpcs(map.Id);
        }
        /// <summary>
        /// 显示当前地图NPC
        /// </summary>
        /// <param name="roomId"></param>
        public static void ShowRoomNpcs(int roomId)
        {
            var roomNpcs = Sims.Context.WorldData.ActiveNpcs.Where(x => x.MapId == roomId).ToList();
            if (roomNpcs.Any())
            {
                UI.Print("这里有");
                for (int i = 0; i < roomNpcs.Count; i++)
                {
                    UI.Print(roomNpcs[i].Name, ConsoleColor.Blue);
                    if (i < roomNpcs.Count - 1)
                    {
                        UI.Print("、");
                    }
                }
            }

        }

        public static void ShowRoomItems(int roomId)
        {

        }

        /// <summary>
        /// 帮助信息
        /// </summary>
        /// <param name="commands"></param>
        public static void HelpInfo(string[] commands)
        {
            UI.PrintLine("系统命令：load/读档 [存档名字] save/存档 [存档名字] exit/退出", ConsoleColor.Magenta);
            UI.PrintLine("特殊命令：command/cmd/命令 显示可操作命令", ConsoleColor.Magenta);
            UI.PrintLine("特殊命令：look/观察 查看周围，将显示环境信息和当前地图的人物、可交互物品", ConsoleColor.Magenta);
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

        #region Spectre.Console

        public static RaceEnum ChooseRace()
        {
            var raceList = EnumExtension.ToListItems(typeof(RaceEnum));
            if (raceList.Any())
            {
                var items = raceList.Select(x => x.Text).ToArray();
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
                var items = enums.Select(x => x.Text).ToArray();
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

    }
}
