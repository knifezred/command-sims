using CommandSims.Core;
using CommandSims.Entity.Npc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.PokerCards
{
    public class PokerRobot
    {
        public static LandloarActionEnum NormalRobotCallLoard(CardPlayer player)
        {
            var call = LandloarActionEnum.pass;
            var sumPoint = player.Cards.Sum(x => x.Point);
            if (sumPoint > 260)
            {
                call = LandloarActionEnum.call;
            }
            if (player.Cards.Any(x => x.Point == 54 && x.Point == 53))
            {
                call = LandloarActionEnum.doubleCall;
            }
            if (sumPoint > 300)
            {
                call = LandloarActionEnum.doubleCall;
            }
            return call;
        }

        public static string NormalPlay(CardPlayer player, string tableCardGroup)
        {

            if (tableCardGroup == "")
            {
                tableCardGroup = FirstHandNormal(player);
            }
            else
            {
                tableCardGroup = PressOnNormal(player, tableCardGroup);
            }
            return tableCardGroup;

        }

        public static string FirstHandNormal(CardPlayer player)
        {
            var tableCard = "";
            var tempCards = player.Cards.OrderBy(x => x.Point);
            // 2. 9以内优先出手
            var tempPoint = 0;
            var shunziCard = "";
            foreach (var item in tempCards)
            {
                // 三带
                if (tempCards.Count(x => x.Point == item.Point) == 3)
                {
                    continue;
                }
                // 对子
                if (tempCards.Count(x => x.Point == item.Point) == 2)
                {
                    if (item.Point < 9)
                    {
                        player.Cards.RemoveAll(x => x.Point == item.Point);
                        UI.PrintLine(player.Name + " - " + item.Name + " " + item.Name);
                        tableCard = item.Value[..1] + item.Value[..1];
                        break;
                    }
                }
                // 单张
                if (tempCards.Count(x => x.Point == item.Point) == 1)
                {
                    //能否组顺子
                    if (tempCards.Count(x => x.Point == item.Point &&
                    x.Point == (item.Point + 1) &&
                    x.Point == (item.Point + 2) &&
                    x.Point == (item.Point + 3) &&
                    x.Point == (item.Point + 4)
                    ) > 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (item.Point < 9)
                        {
                            player.Cards.RemoveAll(x => x.Point == item.Point);
                            UI.PrintLine(player.Name + " - " + item.Name);
                            break;
                        }
                    }
                }

            }
            return tableCard;
        }

        public static string PressOnNormal(CardPlayer player, string tableCardGroup)
        {
            var tableCard = "";
            //单张
            if (tableCardGroup.Length == 1)
            {
                var card = PokerEngine.SuitCards.First(x => x.Value.Contains(tableCardGroup));
                var bigCards = player.Cards.Where(x => x.Point > card.Point).OrderBy(x => x.Point).ToList();
                //验证其他组
                foreach (var item in bigCards)
                {
                    if (player.Cards.Count(x => x.Point == item.Point) == 1)
                    {
                        tableCard = item.Value[..1];
                        OutCardGroup(player, tableCard);
                        break;
                    }
                }
            }

            if (tableCardGroup.Length == 2)
            {
                if (tableCardGroup.Contains("X") || tableCardGroup.Contains("x"))
                {
                    //王炸,要不起
                }
                else
                {
                    var card = PokerEngine.SuitCards.First(x => x.Value.Contains(tableCardGroup));
                    var bigCards = player.Cards.Where(x => x.Point > card.Point).OrderBy(x => x.Point).ToList();
                    foreach (var item in bigCards)
                    {
                        if (player.Cards.Count(x => x.Point == item.Point) == 2)
                        {
                            tableCard = item.Value[..1] + item.Value[..1];
                            OutCardGroup(player, tableCard);
                            break;
                        }
                    }
                }
            }
            if (tableCard == "")
            {
                UI.PrintLine($"{player.Name}- 要不起");
                tableCard = tableCardGroup;
            }
            return tableCard;
        }

        public static void OutCardGroup(CardPlayer player, string cardGroup)
        {
            var card = player.Cards.First(x => x.Value.Contains(cardGroup));
            player.Cards.Remove(card);
            UI.PrintLine($"{player.Name}-{card.Name}");
        }

        public void RebuildCardGroup(List<CardEntity> cards)
        {
            // 1. 组牌优先级 炸弹 顺子 三带 连对 对子 单牌
            var tempCards = cards.OrderBy(x => x.Point).ToList();


        }
    }
}
