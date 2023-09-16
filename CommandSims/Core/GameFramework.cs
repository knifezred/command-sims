using CommandSims.Constants;
using CommandSims.Entity;
using CommandSims.Entity.Npc;
using CommandSims.Enums;
using CommandSims.Modules.Archive;
using CommandSims.Modules.Events;
using CommandSims.Modules.Maps;
using CommandSims.Modules.Talents;
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
        public List<EventEntity> EventList { get; set; }
        public List<Talent> TalentList { get; set; }

        public GameFramework()
        {
        }
        public void AfterLoad()
        {
            TalentList = new TalentData().Talents;
            EventList = new EventsData().Events;
        }

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
            Sims.Context.Name = saveName;
            Sims.Context.SavedTime = DateTime.Now;
            Sims.Context.WorldData.WorldTime = Sims.WorldTime;
            var data = JsonSerializer.Serialize(Sims.Context);
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
                var archiveData = JsonSerializer.Deserialize<ArchiveContext>(archiveDataText);
                if (archiveData != null)
                {
                    UI.PrintLine("存档加载成功");
                    Sims.Reload(archiveData);
                    Sims.World.GoRandomWeather();
                    UI.ShowPlayerInfo(0);
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
                        UI.StartPanel();
                        break;
                    case "command":
                    case "cmd":
                    case "命令":
                        PlayerCommandAction();
                        break;
                    case "look":
                    case "观察":
                        UI.ShowMapInfo(0);
                        break;
                    case "move":
                    case "map":
                    case "地图":
                        UI.ShowMoveMap(0);
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
                        UI.PrintLine("无效指令，请重新输入,help查看可用指令", ConsoleColor.DarkGray);
                        break;
                }

            }
            if (eventId > 0)
            {
                // 释放UI
                UI.Stop();
                ReadCommand(Console.ReadLine(), eventId);
            }
        }

        /// <summary>
        /// 玩家操作命令
        /// TODO
        /// </summary>
        public void PlayerCommandAction()
        {
            var action = UI.EnumSelect<PlayerActionEnum>("[green]你可进行如下操作[/]");
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
            var player = Sims.Context.Player;
            if (playerId > 0)
            {
                //npc
                player = Sims.World.NpcList.FirstOrDefault(x => x.Id == playerId);
            }
            if (action == PlayerActionEnum.Attack)
            {

            }
            result = true;
            return result;
        }
        /// <summary>
        /// todo
        /// </summary>
        /// <param name="playerId"></param>
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

        #region 骰子判定
        public int RollDice(int maxPoint = 26)
        {
            var result = RandomUtils.Next(maxPoint);
            return result + Sims.Context.Player.Attribute.Lucky;
        }

        public bool RollTheDice(int successPoint, int maxPoint = 26)
        {
            var result = RandomUtils.Next(maxPoint);
            return result > successPoint;
        }

        /// <summary>
        /// 检查背包是否存在物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public bool CheckItemInBag(int itemId, int playerId = 0)
        {
            return true;
        }
        #endregion

        #region 地图

        /// <summary>
        /// 地图移动，一次向周围8个方位移动一格
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool MapMove(MoveDirection direction)
        {
            if (direction == MoveDirection.Center)
            {
                return false;
            }
            MapEntity nextMap = new();
            var maps = Sims.World.Map.GetArroundMaps(Sims.Context.CurrentMap.Id);
            var currentMap = Sims.Context.CurrentMap;
            var directionSrt = Enum.GetName(typeof(MoveDirection), direction);
            switch (direction)
            {
                case MoveDirection.Right:
                    nextMap = maps.FirstOrDefault(x => x.LocationX == (currentMap.LocationX + 1) && x.LocationY == currentMap.LocationY);
                    break;
                case MoveDirection.Down:
                    nextMap = maps.FirstOrDefault(x => x.LocationX == currentMap.LocationX && x.LocationY == (currentMap.LocationY - 1));
                    break;
                case MoveDirection.Left:
                    nextMap = maps.FirstOrDefault(x => x.LocationX == (currentMap.LocationX - 1) && x.LocationY == currentMap.LocationY);
                    break;
                case MoveDirection.Up:
                    nextMap = maps.FirstOrDefault(x => x.LocationX == currentMap.LocationX && x.LocationY == (currentMap.LocationY + 1));
                    break;
                case MoveDirection.RightUp:
                    nextMap = maps.FirstOrDefault(x => x.LocationX == (currentMap.LocationX + 1) && x.LocationY == (currentMap.LocationY + 1));
                    break;
                case MoveDirection.RightDown:
                    nextMap = maps.FirstOrDefault(x => x.LocationX == (currentMap.LocationX + 1) && x.LocationY == (currentMap.LocationY - 1));
                    break;
                case MoveDirection.LeftUp:
                    nextMap = maps.FirstOrDefault(x => x.LocationX == (currentMap.LocationX - 1) && x.LocationY == (currentMap.LocationY + 1));
                    break;
                case MoveDirection.LeftDown:
                    nextMap = maps.FirstOrDefault(x => x.LocationX == (currentMap.LocationX - 1) && x.LocationY == (currentMap.LocationY - 1));
                    break;
                case MoveDirection.Center:
                    nextMap = currentMap;
                    break;
                case MoveDirection.Enter:
                    nextMap = Sims.World.Map.GoMapEnterRoom(currentMap.Id);
                    break;
                case MoveDirection.Exit:
                    nextMap = Sims.World.Map.GetMapById(currentMap.ParentId);
                    break;
                default:
                    nextMap = currentMap;
                    break;
            }
            if (nextMap != null)
            {
                Sims.Context.CurrentMap = nextMap;
            }
            return true;
        }

        #endregion

        #region 事件处理

        public bool IsEventAvailable(int id)
        {

            return false;
        }

        public EventEntity GetEvent(string name)
        {
            var events = EventList.Where(x => x.Name == name).ToList();
            var eventIndex = RandomUtils.GetNextWithWeight(events.Select(x => x.Weight).ToList());
            return events[eventIndex];
        }

        public List<EventSelectItem> GetRandomTalents(int maxCount = 10)
        {
            var result = new List<EventSelectItem>();
            var talentsIndexs = RandomUtils.GetNextListWithWeight(TalentList.Select(x => x.Weight).ToList(), maxCount);
            foreach (var index in talentsIndexs)
            {
                var tempTalent = TalentList.ElementAt(index);
                result.Add(new EventSelectItem()
                {
                    EventName = "",
                    Text = "[" + tempTalent.Grade.ToString() + "]" + tempTalent.Name + "[/]," + tempTalent.Description,
                    Value = "[" + tempTalent.Grade.ToString() + "]" + tempTalent.Name + "[/]",
                    TalentId = tempTalent.Id,
                });

            }
            return result;
        }

        public void ActiveEffect(EffectEntity effect, int playerId = 0, Talent? talent = null)
        {
            var player = Sims.GetPlayer(playerId);
            if (effect.Type == EffectEnum.Attribute)
            {
                if (CheckEffectCondition(effect.Condition, player))
                {
                    player.Attribute.Strength += effect.Attribute.Strength;
                    player.Attribute.Perception += effect.Attribute.Perception;
                    player.Attribute.Endurance += effect.Attribute.Endurance;
                    player.Attribute.Charisma += effect.Attribute.Charisma;
                    player.Attribute.Intelligence += effect.Attribute.Intelligence;
                    player.Attribute.Agility += effect.Attribute.Agility;
                    player.Attribute.Lucky += effect.Attribute.Lucky;
                    if (talent != null)
                    {
                        UI.PrintBlueLine("天赋【" + talent.Name + "】触发," + talent.Description);
                    }
                }
            }
            if (effect.Type == EffectEnum.Talent)
            {

            }
        }
        public void ActiveEffects(List<EffectEntity> effects, int playerId = 0, Talent? talent = null)
        {
            foreach (var item in effects)
            {
                ActiveEffect(item, playerId, talent);
            }
        }

        public bool CheckEffectCondition(string condition, Player player)
        {
            var result = true;
            if (condition != null && condition.Any())
            {
                var cons = condition.Split("|");
                foreach (var con in cons)
                {
                    //上一轮条件验证失败
                    if (result == false)
                    {
                        break;
                    }
                    result = false;
                    var corns = con.Split(":");
                    int val = 0;
                    if (corns[0].ToLower() == "age")
                    {
                        val = player.Age;
                    }
                    if (corns[0].ToLower() == "gender")
                    {
                        val = (int)player.Gender;
                    }
                    result = CoditionValueCompare(val, corns[1]);
                }
            }
            return result;
        }
        #endregion

        public bool CoditionValueCompare(int val, string corn)
        {
            var op = corn[..1];
            var val2 = corn[1..];
            if (op == "=")
            {
                if (val == int.Parse(val2))
                {
                    return true;
                }
            }
            if (op == ">")
            {
                if (val > int.Parse(val2))
                {
                    return true;
                }
            }
            if (op == "<")
            {
                if (val < int.Parse(val2))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
