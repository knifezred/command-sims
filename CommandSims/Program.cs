using CommandSims.Constants;
using CommandSims.Core;
using CommandSims.Data;
using Spectre.Console;

internal class Program
{
    private static void Main(string[] args)
    {
        Sims.StartInit();
        SpectreConsoleDemo();
        UI.LoadStartPanel();
        // 监听全局事件
        ReadCommand(Console.ReadLine(), 1);
    }

    static void SpectreConsoleDemo()
    {
        if (!AnsiConsole.Confirm("Run prompt example?"))
        {
            AnsiConsole.MarkupLine("Ok... :(");
            return;
        }
        var layout = new Layout("Root")
            .SplitColumns(
            new Layout("Left"),
            new Layout("Right").SplitRows(
                new Layout("Top"),
                new Layout("Bottom")));

        // Update the left column
        layout["Left"].Update(
            new Panel(
                Align.Center(
                    new Markup("Hello [blue]World![/]"),
                    VerticalAlignment.Middle))
                .Expand());

        layout["Top"].Update(new Panel(new BarChart()
            .Label("[green bold underline]Number of fruits[/]")
            .CenterLabel()
            .WithMaxValue(100)
            .AddItem("Apple", 12, Color.Yellow)
            .AddItem("Orange", 54, Color.Green)
            .AddItem("Banana", 33, Color.Red)).Expand());
        layout["Bottom"].Update(new Panel(
            new Calendar(2020, 10).HeaderStyle(Style.Parse("blue"))
            ).Expand());
        // Render the layout
        AnsiConsole.Write(layout);

        AnsiConsole.Write(new BreakdownChart()
            .FullSize()
            .AddItem("SCSS", 80, Color.Red)
            .AddItem("HTML", 28.3, Color.Blue)
            .AddItem("C#", 22.6, Color.Green)
            .AddItem("JavaScript", 6, Color.Yellow)
            .AddItem("Ruby", 6, Color.LightGreen)
            .AddItem("Shell", 0.1, Color.Aqua));

        AnsiConsole.Write(new Rule("[red]Hello[/]"));


        var name = AnsiConsole.Ask<string>("What's your [green]name[/]?");
        var gender = UI.ChooseGender();
        var race = UI.ChooseRace();
        Sims.PlayerData.PlayerInfo.Name = name;
        Sims.PlayerData.PlayerInfo.Gender = gender;
        Sims.PlayerData.PlayerInfo.Race = race;
    }


    static void ReadCommand(string? command, int eventId)
    {
        if (command != null)
        {
            var commands = command.Split(" ");
            switch (commands[0])
            {
                case "help":
                    HelpInfo(commands);
                    break;
                case "load":
                case "读档":
                    Sims.GameFramework.LoadArchive(commands[1]);
                    break;
                case "save":
                case "存档":
                    Sims.GameFramework.SaveArchive(commands[1]);
                    break;
                case "exit":
                case "退出":
                    UI.PrintLine("自动保存中...");
                    Sims.GameFramework.SaveArchive("AutoSaved");
                    UI.Print("shutdown this program...");
                    eventId = 0;
                    break;
                case "open":
                case "o":
                case "打开":

                    break;
                case "learn":
                case "l":
                case "学":
                    break;
                case "bag":
                    UI.PrintLine("打开了背包");
                    break;
                default:
                    UI.PrintLine("无效指令，请重新输入", ConsoleColor.DarkGray);
                    break;
            }

        }
        if (eventId > 0)
        {
            ReadCommand(Console.ReadLine(), eventId);
        }
    }

    /// <summary>
    /// 帮助信息
    /// </summary>
    /// <param name="commands"></param>
    static void HelpInfo(string[] commands)
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