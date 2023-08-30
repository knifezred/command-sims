using CommandSims.Constants;
using CommandSims.Data;
using CommandSims.Entity.Archive;
using CommandSims.Enums;
using CommandSims.Stories;
using CommandSims.Utils;
using KnifeZ.Unity.Extensions;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace CommandSims.Core
{
    public class GameFramework
    {
        #region 存档
        /// <summary>
        /// 存档
        /// </summary>
        /// <param name="saveName"></param>
        public void SaveArchive(string saveName)
        {
            UI.PrintLine("存档保存中...");
            if (!saveName.Any())
            {
                saveName = "AutoSaved";
            }
            var archivePath = Path.Join(PathConst.ARCHIVE_PATH, saveName);
            var data = JsonSerializer.Serialize(Sims.PlayerData);
            FileUtils.WriteFile(data, archivePath);
            UI.PrintLine("存档保存成功");
        }

        /// <summary>
        /// 读档
        /// </summary>
        /// <param name="archiveName"></param>
        public void LoadArchive(string archiveName)
        {
            UI.PrintLine("存档读取中...");
            var archives = FileUtils.List(PathConst.ARCHIVE_PATH);
            // todo 读取存档文件
            FileInfo? archive;
            if (archiveName == "")
            {
                archive = archives.OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
            }
            else
            {
                archive = archives.AsQueryable().Where(x => x.Name == archiveName).FirstOrDefault();
            }
            if (archive != null)
            {
                var archiveDataText = FileUtils.ReadFile(archive.FullName);
                var archiveData = JsonSerializer.Deserialize<ArchiveData>(archiveDataText);
                if (archiveData != null)
                {
                    UI.PrintLine("存档加载成功");
                    Sims.PlayerData = archiveData;
                    WorldFramework.WorldTime = archiveData.WorldTime;
                    Sims.World.GoRandomWeather();
                    UI.ShowPalyerInfo();
                }
                else
                {
                    UI.PrintLine("存档读取失败，请重新开档或读取其他存档");
                }
            }
            else
            {
                var result = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[[" + archiveName + "]]不存在，是否创建新存档？")
                    .PageSize(10)
                    .AddChoices(new string[]
                    {
                        "1. 确定","2. 读取其他存档"
                    }));
                if (result.StartsWith("1"))
                {
                    Console.WriteLine();
                    new S0_SomeoneBorned().PlayerBorn();
                }
                else if (result.StartsWith("2"))
                {
                    UI.PrintLine("请输入存档名称");
                    var newArchive = Console.ReadLine();
                    if (newArchive != null)
                    {
                        LoadArchive(newArchive);
                    }
                }
                else
                {
                    UI.PrintLine("无法识别的输入选项，将退回");
                }
            }

        }

        #endregion

        #region 通用指令

        public void ReadCommand(string? command, int eventId)
        {
            if (command != null)
            {
                var commands = command.Split(" ");
                switch (commands[0])
                {
                    case "help":
                        UI.HelpInfo(commands);
                        break;
                    case "load":
                    case "读档":
                        Sims.Game.LoadArchive(commands.Length > 1 ? commands[1] : "AutoSaved");
                        break;
                    case "save":
                    case "存档":
                        Sims.Game.SaveArchive(commands.Length > 1 ? commands[1] : "AutoSaved");
                        break;
                    case "exit":
                    case "退出":
                        Sims.Game.SaveArchive("AutoSaved");
                        eventId = 0;
                        break;
                    case "command":
                    case "cmd":
                    case "命令":
                        PlayerCommandAction();
                        break;
                    case "look":
                    case "观察":
                        break;
                    case "open":
                    case "o":
                    case "打开":

                        break;
                    case "learn":
                    case "l":
                    case "学":
                        break;
                    default:
                        UI.PrintLine("无效指令，请重新输入", ConsoleColor.DarkGray);
                        break;
                }

            }
            if (eventId > 0)
            {
                UI.SetFree();
                ReadCommand(Console.ReadLine(), eventId);
            }
        }

        /// <summary>
        /// 玩家操作命令
        /// </summary>
        public void PlayerCommandAction()
        {
            var enums = EnumExtension.ToListItems(typeof(PlayerActionEnum));
            var actions = enums.Select(x => x.Text.ToString().ToString()).ToArray();
            var result = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[green]你可进行如下操作[/]")
                .PageSize(10)
                .AddChoices(actions));
            var action = EnumExtension.GetValueFromName<PlayerActionEnum>(result);
            switch (action)
            {
                case PlayerActionEnum.DoNothing:
                    UI.PrintLine("你四下打量，决定什么也不做");
                    break;
                case PlayerActionEnum.Attack:
                    CommandAttack(0);
                    // todo 选择攻击对象
                    break;
                default:
                    UI.PrintLine("尚未开发,敬请期待");
                    break;
            }

        }

        /// <summary>
        /// 命令执行预检
        /// </summary>
        /// <param name="action">命令</param>
        /// <param name="playerId">玩家ID</param>
        /// <returns></returns>
        public bool PreCommandActionCheck(PlayerActionEnum action, int playerId)
        {
            bool result = false;
            var player = Sims.PlayerData.PlayerInfo;
            if (playerId > 0)
            {
                //npc
                player = Sims.NpcList.FirstOrDefault(x => x.Id == playerId);
            }
            if (action == PlayerActionEnum.Attack)
            {

            }
            result = true;
            return result;
        }

        public void CommandAttack(int playerId)
        {
            var canDo = PreCommandActionCheck(PlayerActionEnum.Attack, 0);
            if (canDo)
            {
                UI.PrintLine("你决定攻击...");

            }
            else
            {

            }

        }

        #endregion


    }
}
