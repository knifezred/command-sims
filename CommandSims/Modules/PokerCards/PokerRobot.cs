using CommandSims.Core;
using CommandSims.Entity.Npc;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
            if (tableCardGroup == "" || player.LastOutCard == tableCardGroup)
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
            foreach (var item in tempCards)
            {
                // 单张
                if (tempCards.Count(x => x.Point == item.Point) == 1)
                {
                    //能否组顺子
                    if (CanBeShunzi(player.Cards, item))
                    {
                        // Normal不管优先级，有单出单，能组顺子就跳过
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
                // 三带
                if (tempCards.Count(x => x.Point == item.Point) == 3)
                {
                    if (item.Point < 10)
                    {

                    }
                    continue;
                }
                if (tempCards.Count(x => x.Point == item.Point) == 4)
                {
                    continue;
                }

            }
            if (tableCard != "")
            {
                player.LastOutCard = tableCard;
                PokerEngine.CurrentHandPlayer = player;
            }
            return tableCard;
        }

        private static bool CanBeShunzi(List<CardEntity> cards, CardEntity card)
        {
            var shunziCard = "";
            var isShunzi = false;
            for (int i = card.Point; i < card.Point + 5; i++)
            {
                // 重定义A牌point
                if (i == 14)
                {
                    i = 20;
                }
                var next = cards.Where(x => x.Point == i).FirstOrDefault();
                if (next != null)
                {
                    if (cards.Count(x => x.Point == i) < 3)
                    {
                        shunziCard += next.Value[..1];
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            for (int i = card.Point; i < card.Point - 5; i--)
            {
                if (i <= 1)
                {
                    break;
                }
                var next = cards.Where(x => x.Point == i).FirstOrDefault();
                if (next != null)
                {
                    if (cards.Count(x => x.Point == i) < 3)
                    {
                        shunziCard += next.Value[..1];
                    }
                }
                else
                {
                    break;
                }
            }
            if (shunziCard.Length > 5)
            {
                isShunzi = true;
            }
            return isShunzi;
        }

        private static string ThrowBoom(List<CardEntity> cards, int minPoint = 0)
        {
            var tableCard = "";
            foreach (var item in cards)
            {
                if (cards.Count(x => x.Point == item.Point) == 4 && item.Point > minPoint)
                {
                    tableCard = item.Value[..1] + item.Value[..1] + item.Value[..1] + item.Value[..1];
                    break;
                }
            }
            return tableCard;

        }
        public static string PressOnNormal(CardPlayer player, string tableCardGroup)
        {
            var tableCard = "";
            // 单张
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
                if (card.Point > 13 && tableCard == "")
                {
                    tableCard = ThrowBoom(player.Cards);
                }
            }
            // 对子
            if (tableCardGroup.Length == 2)
            {
                if (tableCardGroup.Contains("X") || tableCardGroup.Contains("x"))
                {
                    //王炸,要不起
                }
                else
                {
                    var card = PokerEngine.SuitCards.First(x => x.Value.Contains(tableCardGroup[..1]));
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
                    if (card.Point > 10 && tableCard == "")
                    {
                        tableCard = ThrowBoom(player.Cards);
                    }
                }
            }
            // 三不带
            if (tableCardGroup.Length == 3)
            {
                var card = PokerEngine.SuitCards.First(x => x.Value.Contains(tableCardGroup[..1]));
                var bigCards = player.Cards.Where(x => x.Point > card.Point).OrderBy(x => x.Point).ToList();
                foreach (var item in bigCards)
                {
                    if (player.Cards.Count(x => x.Point == item.Point) == 3)
                    {
                        tableCard = item.Value[..1] + item.Value[..1];
                        OutCardGroup(player, tableCard);
                        break;
                    }
                }
            }
            // 连对 三带一 炸弹
            if (tableCardGroup.Length == 4)
            {
                var cardPoints = tableCardGroup.ToArray();
                Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
                foreach (var card in cardPoints)
                {
                    if (keyValuePairs.ContainsKey(card.ToString()))
                    {
                        keyValuePairs[card.ToString()]++;
                    }
                    else
                    {
                        keyValuePairs.Add(card.ToString(), 1);
                    }
                }

                if (keyValuePairs.Count == 2)
                {
                    if (keyValuePairs.ContainsValue(2))
                    {
                        // 连对
                        int point = 0;
                        foreach (var item in keyValuePairs)
                        {
                            if (PokerEngine.SuitCards.First(x => x.Value.Contains(item.Key)).Point > point)
                            {
                                point = PokerEngine.SuitCards.First(x => x.Value.Contains(item.Key)).Point;
                            }
                        }
                        var bigCards = player.Cards.Where(x => x.Point > point).OrderBy(x => x.Point).ToList();
                        foreach (var item in bigCards)
                        {
                            if (player.Cards.Count(x => x.Point == item.Point) >= 2)
                            {
                                var next = bigCards.FirstOrDefault(x => x.Point == item.Point++);
                                if (next != null && player.Cards.Count(x => x.Point == item.Point++) >= 2)
                                {
                                    tableCard = item.Value[..1] + item.Value[..1];
                                    tableCard += next.Value[..1] + next.Value[..1];
                                }
                            }
                        }

                    }
                    else
                    {
                        // 三带一
                        var minCard = PokerEngine.SuitCards.First(x => x.Value.Contains(keyValuePairs.First(x => x.Value == 3).Key));
                        var bigCards = player.Cards.Where(x => x.Point > minCard.Point).OrderBy(x => x.Point).ToList();
                        foreach (var item in bigCards)
                        {
                            if (player.Cards.Count(x => x.Point == item.Point) == 3)
                            {
                                foreach (var next in player.Cards)
                                {

                                    if (player.Cards.Count(x => x.Point == next.Point) == 1)
                                    {
                                        //能否组顺子
                                        if (CanBeShunzi(player.Cards, item))
                                        {
                                            continue;
                                        }
                                        tableCard = item.Value[..1] + item.Value[..1] + item.Value[..1];
                                        tableCard += next.Value[..1];
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }   //炸弹
                if (keyValuePairs.Count == 1)
                {
                    tableCard = ThrowBoom(player.Cards, PokerEngine.SuitCards.First(x => x.Value.Contains(tableCardGroup[..1])).Point);
                }
                else if (tableCard == "")
                {
                    tableCard = ThrowBoom(player.Cards);
                }

            }
            // 顺子 飞机 连对 三带二
            if (tableCardGroup.Length > 5)
            {

            }
            if (tableCard == "")
            {
                UI.PrintLine($"{player.Name}- 要不起");
                tableCard = tableCardGroup;
                player.LastOutCard = "";
            }
            else
            {
                player.LastOutCard = tableCard;
                PokerEngine.CurrentHandPlayer = player;
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
