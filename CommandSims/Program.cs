using CommandSims.Constants;
using CommandSims.Core;
using CommandSims.Data;
using CommandSims.Entity.Archive;
using CommandSims.Stories;
using CommandSims.Utils;
using Spectre.Console;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("Welcome to [green]command sims[/]!");
        UI.PrintLine("请输入存档名称或开启新存档", ConsoleColor.DarkGray);
        // 加载事件
        ReadCommand(Console.ReadLine(), 1);
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
                    LoadProfileArchive(commands.Skip(1).ToList());
                    break;
                case "save":
                case "存档":

                    break;
                case "exit":
                case "退出":
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

    static void LoadProfileArchive(List<string> commands)
    {
        using var db = new SimsContext();
        foreach (string command in commands)
        {
            var archives = FileUtils.List(PathConst.ARCHIVE_PATH);
            // todo 读取存档文件
            var archive = archives.AsQueryable().Where(x => x.Name == command).FirstOrDefault();
            if (archive != null)
            {
                var archiveDataText = FileUtils.ReadFile(archive.FullName);

                CurrentActivityData activityData = new CurrentActivityData();
                var archiveData = JsonSerializer.Deserialize<ArchiveData>(archiveDataText);
                if (archiveData != null)
                {
                    activityData.ArchiveData = archiveData;
                    UI.PrintLine("存档加载成功");
                }
                else
                {
                    UI.PrintLine("存档读取失败，请重新开档或读取其他存档");
                }
            }
            else
            {
                UI.PrintLine("[" + command + "]不存在，是否创建新存档？");
                UI.PrintLine("1. 确定 2. 读取其他存档");
                var choose = Console.ReadKey();
                if (choose.KeyChar.ToString() == "1")
                {
                    Console.WriteLine();
                    SomeoneBorned someoneBorned = new();
                    someoneBorned.PlayerBorn();
                }
                else if (choose.KeyChar.ToString() == "2")
                {
                    UI.PrintLine("请输入存档名称");
                    var archiveName = Console.ReadLine();
                    if (archiveName != null)
                    {
                        LoadProfileArchive(new List<string> { archiveName });
                    }
                }
                else
                {
                    UI.PrintLine("无法识别的输入选项，将退回");
                }
            }
        }
    }

}