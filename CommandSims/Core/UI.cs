using CommandSims.Entity.Base;
using CommandSims.Enums;
using CommandSims.Modules.Maps;
using CommandSims.Stories;
using CommandSims.Utils;
using KnifeZ.Unity.Extensions;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// <summary>
        /// 开始输出
        /// </summary>
        public static void StartWork()
        {
            Busy = true;
        }
        /// <summary>
        /// 停止输出
        /// </summary>
        public static void StopWork()
        {
            Busy = false;
        }
        #endregion

        #region 地图
        private static Table LiveTable;
        public static void LiveMap(int mapId = 0)
        {
            LiveTable = new Table();
            LiveTable.HideHeaders();
            LiveTable.Border(TableBorder.Ascii);
            LiveTable.Alignment(Justify.Center);
            LiveTable.Centered();
            LiveTable.AddColumn("西");
            LiveTable.AddColumn("");
            LiveTable.AddColumn("东");
            foreach (var column in LiveTable.Columns)
            {
                column.Centered();
                column.Width = 18;
            }
            Console.WriteLine();
            Console.WriteLine();
            AnsiConsole.Live(LiveTable)
                .Start(ctx =>
                {
                    UpdateMapRows(mapId, ctx);
                });
        }

        private static void UpdateMapRows(int mapId, LiveDisplayContext ctx)
        {
            var crrentMap = Sims.World.Map.GetMapById(mapId);
            var maps = Sims.World.Map.GetArroundMaps(crrentMap.Id).OrderBy(x => x.LocationX).ThenByDescending(x => x.LocationY).ToList();
            Sims.Context.CurrentMap = crrentMap;
            List<MoveDirection> moveDirections = new()
            {
                MoveDirection.Center
            };
            if (Sims.World.Map.CanEnter(crrentMap))
            {
                moveDirections.Add(MoveDirection.Enter);
            }
            if (Sims.World.Map.CanExit(crrentMap))
            {
                moveDirections.Add(MoveDirection.Exit);
            }
            var rows = new List<string[]>();
            for (int i = 0; i < 3; i++)
            {
                LiveTable.AddRow(new string[3] { "", "", "" });
                rows.Add(new string[3] { "", "", "" });
            }
            var tempX = Sims.Context.CurrentMap.LocationX;
            var tempY = Sims.Context.CurrentMap.LocationY;
            foreach (var map in maps)
            {
                if (map.LocationX < tempX && map.LocationY == tempY)
                {
                    moveDirections.Add(MoveDirection.Left);
                    rows[1][0] = map.Name;
                    LiveTable.UpdateCell(1, 0, map.Name);
                }
                if (map.LocationX == tempX && map.LocationY == tempY)
                {
                    rows[1][1] = "[red]" + map.Name + "[/]";
                    LiveTable.UpdateCell(1, 1, "[red]" + map.Name + "[/]");
                }
                if (map.LocationX > tempX && map.LocationY == tempY)
                {
                    moveDirections.Add(MoveDirection.Right);
                    rows[1][2] = map.Name;
                    LiveTable.UpdateCell(1, 2, map.Name);
                }

                if (map.LocationX < tempX && map.LocationY > tempY)
                {
                    moveDirections.Add(MoveDirection.LeftUp);
                    rows[0][0] = map.Name;
                    LiveTable.UpdateCell(0, 0, map.Name);
                }
                if (map.LocationX == tempX && map.LocationY > tempY)
                {
                    moveDirections.Add(MoveDirection.Up);
                    rows[0][1] = map.Name;
                    LiveTable.UpdateCell(0, 1, map.Name);
                }
                if (map.LocationX > tempX && map.LocationY > tempY)
                {
                    moveDirections.Add(MoveDirection.RightUp);
                    rows[0][2] = map.Name;
                    LiveTable.UpdateCell(0, 2, map.Name);
                }

                if (map.LocationX < tempX && map.LocationY < tempY)
                {
                    moveDirections.Add(MoveDirection.LeftDown);
                    rows[2][0] = map.Name;
                    LiveTable.UpdateCell(2, 0, map.Name);
                }
                if (map.LocationX == tempX && map.LocationY < tempY)
                {
                    moveDirections.Add(MoveDirection.Down);
                    rows[2][1] = map.Name;
                    LiveTable.UpdateCell(2, 1, map.Name);
                }
                if (map.LocationX > tempX && map.LocationY < tempY)
                {
                    moveDirections.Add(MoveDirection.RightDown);
                    rows[2][2] = map.Name;
                    LiveTable.UpdateCell(2, 2, map.Name);
                }

            }
            ctx.Refresh();
            var readKey = Console.ReadKey();
            var direction = (MoveDirection)int.Parse(readKey.KeyChar.ToString());
            Sims.Game.MapMove(direction);
            if (direction != MoveDirection.Center)
            {
                // 除非选择留在此处，否则继续触发移动选项
                UpdateMapRows(0, ctx);
            }
        }

        public static void ShowMoveMap(int mapId = 0)
        {
            var crrentMap = Sims.World.Map.GetMapById(mapId);
            var maps = Sims.World.Map.GetArroundMaps(crrentMap.Id).OrderBy(x => x.LocationX).ThenByDescending(x => x.LocationY).ToList();
            Sims.Context.CurrentMap = crrentMap;
            List<MoveDirection> moveDirections = new()
            {
                MoveDirection.Center
            };
            if (Sims.World.Map.CanEnter(crrentMap))
            {
                moveDirections.Add(MoveDirection.Enter);
            }
            if (Sims.World.Map.CanExit(crrentMap))
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
                    rows2[1] = "[red]" + map.Name + "[/]";
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

            table.HideHeaders();
            table.Border(TableBorder.Ascii);
            table.Alignment(Justify.Center);
            table.Centered();
            foreach (var column in table.Columns)
            {
                column.Centered();
                column.Width = 18;
            }
            AnsiConsole.Write(table);
            ShowMoveDirectionSelect(moveDirections);
        }

        public static void ShowMoveDirectionSelect(List<MoveDirection> moveDirections)
        {
            var direction = AnsiConsole.Prompt(new SelectionPrompt<MoveDirection>()
                .Title("[green]你可进行如下操作[/]")
                .PageSize(10)
                .AddChoices(moveDirections)
                .UseConverter(x => x.GetEnumDisplayName()));
            Sims.Game.MapMove(direction);
            UI.ShowMapInfo(0);
            if (direction != MoveDirection.Center)
            {
                // 除非选择留在此处，否则继续触发移动选项
                ShowMoveMap(0);
            }
        }

        public static void ShowMapInfo(int mapId = 0)
        {
            var map = Sims.World.Map.GetMapById(mapId);
            UI.PrintLine(map.Description);
            ShowRoomNpcs(map.Id);
        }

        #endregion

        /// <summary>
        /// 开始面板
        /// </summary>
        public static void LoadStartPanel()
        {
            UI.StartWork();
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
            UI.PrintLine("操作：open/o/打开 ");
            UI.PrintLine("示例 打开背包(下同不再列举)：1. open bag    2. o bag    3. 打开 bag    4. 打开 背包", ConsoleColor.DarkGray);
            UI.PrintLine("操作：use/u/用");
            UI.PrintLine("示例 使用物品：use 荷包蛋 1", ConsoleColor.DarkGray);
            UI.PrintLine("操作：give/g/送");
            UI.PrintLine("示例 赠送物品：送 荷包蛋 1 张三", ConsoleColor.DarkGray);
            UI.PrintLine("操作：drop/d/扔");
            UI.PrintLine("示例 丢弃物品：drop 荷包蛋 1", ConsoleColor.DarkGray);
            UI.PrintLine("操作：trade/t/交易");
            UI.PrintLine("示例 交易物品：trade 荷包蛋 1 张三", ConsoleColor.DarkGray);
            UI.PrintLine("操作：learn/学习");
            UI.PrintLine("示例 学习技能：learn 清风剑谱", ConsoleColor.DarkGray);
            UI.PrintLine("操作：make/m/制作");
            UI.PrintLine("示例 制作物品：make 桃木剑 1", ConsoleColor.DarkGray);
            UI.PrintLine("操作：destroy/毁");
            UI.PrintLine("示例 摧毁物品：destroy 桃木剑 1", ConsoleColor.DarkGray);
        }

        #region Console.Write重写，支持颜色设置，打字机效果

        public static void Tips(string message)
        {
            PrintLine(message, ConsoleColor.DarkGray);
        }

        public static void Print(string message, ConsoleColor color = ConsoleColor.Green)
        {
            UI.StartWork();
            Console.ForegroundColor = color;
            Typewriter(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintLine(string message, ConsoleColor color = ConsoleColor.Green)
        {
            UI.StartWork();
            Console.ForegroundColor = color;
            Typewriter(message);
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Typewriter(string message, int delay = 10)
        {
            if (message != null)
            {
                var msgs = message.ToArray();
                foreach (var msg in msgs)
                {
                    Console.Write($"{msg}");
                    Thread.Sleep(delay);
                }
            }

        }

        #endregion

        #region Spectre.Console

        public static void PrintGrayLine(string message)
        {
            AnsiConsole.MarkupLine($"[gray]{message}[/]");
        }
        public static void PrintGreenLine(string message)
        {
            AnsiConsole.MarkupLine($"[green]{message}[/]");
        }
        public static void PrintBlueLine(string message)
        {
            AnsiConsole.MarkupLine($"[blue]{message}[/]");
        }
        public static void PrintRedLine(string message)
        {
            AnsiConsole.MarkupLine($"[red]{message}[/]");
        }

        public static ComboSelectListItem ComboSelect(List<ComboSelectListItem> items, string title)
        {
            var result = AnsiConsole.Prompt(new SelectionPrompt<ComboSelectListItem>()
                                    .Title(title)
                                    .PageSize(10)
                                    .AddChoices(items.ToArray())
                                    .UseConverter(x => x.Text));

            AnsiConsole.MarkupLine($"{title} you choose {result.Text} !");
            return result;
        }

        public static TEnum EnumSelect<TEnum>(string title = "")
        {
            if (title == "")
            {
                title = typeof(TEnum).GetDescription();
            }
            var items = EnumExtension.ToListItems(typeof(TEnum)).ToArray();
            var result = AnsiConsole.Prompt(new SelectionPrompt<ComboSelectListItem>()
                                    .Title(title)
                                    .PageSize(10)
                                    .AddChoices(items)
                                    .UseConverter(x => x.Text));
            AnsiConsole.MarkupLine($"{title} - {result.Text}");
            return (TEnum)Enum.Parse(typeof(TEnum), result.Value);
        }

        #endregion

    }
}
