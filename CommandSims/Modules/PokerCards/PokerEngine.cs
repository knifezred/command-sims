using CommandSims.Core;
using CommandSims.Entity.Npc;
using CommandSims.Utils;
using KnifeZ.Unity.Extensions;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.PokerCards
{
    public class PokerEngine
    {

        public static List<CardEntity> SuitCards { get; set; }

        public static List<CardPlayer> Players { get; set; }
        public PokerEngine()
        {
            SuitCards = new List<CardEntity>();
            Players = new List<CardPlayer>();
        }

        public void PlayGame()
        {
            CleanCards();
            UI.PrintLine("添加玩家");
            AddinPlayers(3);
            UI.PrintLine("添加牌组");
            AddinOneSuitCards();
            UI.PrintLine("洗牌中...");
            WashCards();
            UI.PrintLine("发牌");
            LicensingCards();
            //
        }

        public void RestartGame()
        {
            UI.PrintLine("洗牌中...");
            WashCards();
            foreach (var item in Players)
            {
                item.Cards = new List<CardEntity>();
            }
            UI.PrintLine("发牌");
            LicensingCards();
        }

        public void AddinOneSuitCards(bool withJoker = true)
        {
            int cardId = 0;
            if (withJoker)
            {
                SuitCards.Add(new CardEntity()
                {
                    Id = cardId++,
                    Name = CardSuit.Poker.GetEnumDisplayName() + "X",
                    Value = "Xx",
                    Point = 54,
                    Suit = CardSuit.Poker
                });
                SuitCards.Add(new CardEntity()
                {
                    Id = cardId++,
                    Name = CardSuit.Poker.GetEnumDisplayName() + "S",
                    Value = "Ss",
                    Point = 53,
                    Suit = CardSuit.Poker
                });
            }
            foreach (CardSuit card in Enum.GetValues(typeof(CardSuit)))
            {
                if (card == CardSuit.Poker)
                {
                    continue;
                }
                SuitCards.Add(new CardEntity()
                {
                    Id = cardId++,
                    Name = card.GetEnumDisplayName() + "A",
                    Value = "Aa1",
                    Point = 20,
                    Suit = card
                });
                SuitCards.Add(new CardEntity()
                {
                    Id = cardId++,
                    Name = card.GetEnumDisplayName() + "2",
                    Value = "2",
                    Point = 25,
                    Suit = card
                });
                for (int i = 3; i < 10; i++)
                {
                    SuitCards.Add(new CardEntity()
                    {
                        Id = cardId++,
                        Name = card.GetEnumDisplayName() + i.ToString(),
                        Value = i.ToString(),
                        Point = i,
                        Suit = card
                    });
                }
                SuitCards.Add(new CardEntity()
                {
                    Id = cardId++,
                    Name = card.GetEnumDisplayName() + 10,
                    Value = "Tt0",
                    Point = 10,
                    Suit = card
                });
                SuitCards.Add(new CardEntity()
                {
                    Id = cardId++,
                    Name = card.GetEnumDisplayName() + "J",
                    Value = "Jj",
                    Point = 11,
                    Suit = card
                });
                SuitCards.Add(new CardEntity()
                {
                    Id = cardId++,
                    Name = card.GetEnumDisplayName() + "Q",
                    Value = "Qq",
                    Point = 12,
                    Suit = card
                });
                SuitCards.Add(new CardEntity()
                {
                    Id = cardId++,
                    Name = card.GetEnumDisplayName() + "K",
                    Value = "Kk",
                    Point = 13,
                    Suit = card
                });
            }
        }

        public void AddinPlayers(int playerCount)
        {
            Players.Clear();
            for (int i = 0; i < playerCount; i++)
            {
                Players.Add(new CardPlayer()
                {
                    Id = i,
                    Name = "Player" + i,
                    HandIndex = i,
                    Cards = new List<CardEntity>(),
                    Point = 100
                });
            }

        }

        public void CleanCards()
        {
            SuitCards.Clear();
        }
        /// <summary>
        /// 洗牌
        /// </summary>
        public void WashCards()
        {
            var rand = new Random();
            var tempCards = new List<CardEntity>();
            var totalCards = SuitCards.Count;
            var indexList = new List<int>();
            for (int i = 0; i < totalCards; i++)
            {
                var index = rand.Next(SuitCards.Count);
                while (indexList.Contains(index))
                {
                    index = rand.Next(SuitCards.Count);
                }
                tempCards.Add(SuitCards[index]);
                indexList.Add(index);
            }
            SuitCards = tempCards;
        }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <param name="playerCount"></param>
        public void LicensingCards()
        {
            var rand = new Random();
            WashCards();
            var handFirst = rand.Next(Players.Count - 1);
            ChangeHandFirst(handFirst);
            landloardIndex = rand.Next(SuitCards.Count - 5);
            LandloadCard = SuitCards[landloardIndex];
            //留底3张
            for (int i = 0; i < SuitCards.Count - 3; i++)
            {
                foreach (var player in Players)
                {
                    player.Cards.Add(SuitCards[i]);
                    i++;
                }
                i--;
            }
            //Console.OutputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.Unicode;
            CardPlayer cardPlayer = Players.First(x => x.Id == 0);
            LoadCards(cardPlayer);
            CallLandloard();
            PlayAction();
            PointWin();
            RestartGame();
        }

        #region 斗地主
        private static int PointRatio = 1;
        public void LoadCards(CardPlayer player)
        {
            Console.WriteLine($"【牌组-{player.Name}】{player.Cards.OrderByDescending(x => x.Point).Select(x => x.Name).ToSepratedString(seperator: " ")}");
        }
        private int landloardIndex = 0;
        private CardEntity LandloadCard;
        public void CallLandloard()
        {
            // 1. 拿到地主牌的先决定要或不要，不要时顺延到下一家
            // 2. 如果所有人都不要,则重新洗牌
            // 3. 叫地主可以选择double或triple
            var handIndex = Players.FindIndex(x => x.Cards.Any(x => x.Id == LandloadCard.Id));
            // 地主牌先叫
            ChangeHandFirst(handIndex);
            bool anyLoard = false;
            foreach (var player in Players)
            {
                var call = LandloarActionEnum.pass;
                if (player.Id == 0)
                {
                    call = UI.EnumSelect<LandloarActionEnum>();
                }
                else
                {
                    call = PokerRobot.NormalRobotCallLoard(player);
                    UI.PrintLine($"{player.Name}-叫地主 {call.GetEnumDisplayName()}");
                }
                if (call != LandloarActionEnum.pass)
                {
                    if (call == LandloarActionEnum.call)
                    {
                        PointRatio = 1;
                    }
                    if (call == LandloarActionEnum.doubleCall)
                    {
                        PointRatio = 2;
                    }
                    if (call == LandloarActionEnum.tripleCall)
                    {
                        PointRatio = 3;
                    }
                    var newHandIndex = Players.FindIndex(x => x.Id == player.Id);
                    UI.PrintLine($"底牌：{SuitCards[^3].Name} {SuitCards[^2].Name} {SuitCards[^1].Name}");
                    player.Cards.Add(SuitCards[^3]);
                    player.Cards.Add(SuitCards[^2]);
                    player.Cards.Add(SuitCards[^1]);
                    ChangeHandFirst(newHandIndex);
                    anyLoard = true;
                    break;
                }
            }
            if (!anyLoard)
            {
                RestartGame();
            }
        }

        public static CardPlayer CurrentHandPlayer { get; set; }

        public void PlayAction()
        {
            string tableCardGroup = "";
            bool gameOver = false;
            while (!gameOver)
            {
                foreach (var player in Players)
                {
                    if (player.Id == 0)
                    {
                        LoadCards(player);
                        tableCardGroup = PlayCardAction(player, tableCardGroup);
                    }
                    else
                    {
                        tableCardGroup = PokerRobot.NormalPlay(player, tableCardGroup);
                    }
                    if (!player.Cards.Any())
                    {
                        // game over
                        gameOver = true;
                        break;
                    }
                }
            }
        }

        public void PointWin()
        {
            // 先手
            var loard = Players[0];
            if (loard.Cards.Any())
            {
                UI.PrintLine("地主输");
                loard.Point -= 10 * PointRatio * 2;
                Players[1].Point += 10 * PointRatio;
                Players[2].Point += 10 * PointRatio;
            }
            else
            {
                UI.PrintLine("地主赢");
                loard.Point += 10 * PointRatio * 2;
                Players[1].Point -= 10 * PointRatio;
                Players[2].Point -= 10 * PointRatio;
            }
            foreach (var player in Players)
            {
                UI.PrintLine($"{player.Name}-{player.Point}");
            }
        }
        /// <summary>
        /// 玩家出牌
        /// </summary>
        /// <param name="player"></param>
        /// <param name="tableGroup"></param>
        /// <returns></returns>

        public string PlayCardAction(CardPlayer player, string tableGroup)
        {
            var cards = Console.ReadLine();
            if (cards == "" || cards == "p" || cards == "P" || cards == "pass" || cards == "PASS")
            {
                cards = "";
                player.LastOutCard = "";
            }
            else
            {
                var cardList = cards.ToArray();
                if (CheckPlay(player, cards, tableGroup))
                {
                    foreach (var item in cardList)
                    {
                        var currentCard = player.Cards.Where(x => x.Value.Contains(item)).ToList();
                        player.Cards.Remove(currentCard.First());
                    }
                }
                else
                {
                    UI.Error("请按规则出牌");
                    PlayCardAction(player, tableGroup);
                }
                player.LastOutCard = cards;
                CurrentHandPlayer = player;
            }
            return cards;

        }

        /// <summary>
        /// 验证出牌是否符合规则
        /// </summary>
        /// <param name="player"></param>
        /// <param name="outCards"></param>
        /// <param name="tableGroup"></param>
        /// <returns></returns>
        public bool CheckPlay(CardPlayer player, string outCards, string tableGroup)
        {
            var result = true;
            var cardPoints = outCards.ToArray();
            foreach (var item in cardPoints)
            {
                int itemCount = cardPoints.Count(x => x == item);
                if (player.Cards.Count(x => x.Value.Contains(item)) < itemCount)
                {
                    result = false;
                    break;
                }
            }
            if (tableGroup != player.LastOutCard)
            {
                // 验证两次牌组一致不一致
                result = CardGroupIsEqual(outCards, tableGroup);
                // 验证大小
                if (result)
                {
                    result = CanHandleTableCardGroup(outCards, tableGroup);
                }
            }
            return result;

        }

        #endregion
        /// <summary>
        /// 改变先手顺序
        /// </summary>
        /// <param name="playerIndex"></param>
        public void ChangeHandFirst(int playerIndex)
        {
            var temp = Players.OrderBy(x => x.HandIndex).ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                if (i < playerIndex)
                {
                    temp[i].HandIndex = 1 + i + Players.Count - playerIndex;
                }
                else
                {
                    temp[i].HandIndex = 1 + i - playerIndex;
                }
            }
            Players = temp.OrderBy(x => x.HandIndex).ToList();
        }

        /// <summary>
        /// 验证出牌类型是否一致
        /// </summary>
        /// <param name="outCardGroup"></param>
        /// <param name="tableCardGroup"></param>
        /// <returns></returns>
        public bool CardGroupIsEqual(string outCardGroup, string tableCardGroup)
        {
            return GetCardGroupType(outCardGroup) == GetCardGroupType(tableCardGroup);
        }
        /// <summary>
        /// 压牌
        /// </summary>
        /// <param name="outCardGroup"></param>
        /// <param name="tableCardGroup"></param>
        /// <returns></returns>
        public bool CanHandleTableCardGroup(string outCardGroup, string tableCardGroup)
        {
            // todo 
            Dictionary<string, int> outCardGroupDict = new Dictionary<string, int>();
            foreach (var card in outCardGroup.ToArray())
            {
                if (outCardGroupDict.ContainsKey(card.ToString()))
                {
                    outCardGroupDict[card.ToString()]++;
                }
                else
                {
                    outCardGroupDict.Add(card.ToString(), 1);
                }
            }
            Dictionary<string, int> tableCardGroupDict = new Dictionary<string, int>();
            foreach (var card in tableCardGroup.ToArray())
            {
                if (tableCardGroupDict.ContainsKey(card.ToString()))
                {
                    tableCardGroupDict[card.ToString()]++;
                }
                else
                {
                    tableCardGroupDict.Add(card.ToString(), 1);
                }
            }
            if (outCardGroupDict.Count == 1)
            {
                var card = SuitCards.First(x => x.Value.Contains(outCardGroupDict.ElementAt(0).Key));
                var tableCard = SuitCards.First(x => x.Value.Contains(tableCardGroupDict.ElementAt(0).Key));
                if (card.Point > tableCard.Point)
                {
                    return true;
                }
            }
            if (outCardGroupDict.Count == 2)
            {

            }

            return false;

        }

        public CardGroupType GetCardGroupType(string cardGroup)
        {
            var cardGroupType = CardGroupType.BadCard;
            var cardPoints = cardGroup.ToArray();
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
            if (keyValuePairs.Count == 1)
            {
                if (keyValuePairs.First().Value == 1)
                {
                    return CardGroupType.Single;
                }
                if (keyValuePairs.First().Value == 2)
                {
                    return CardGroupType.Double;
                }
                if (keyValuePairs.First().Value == 3)
                {
                    return CardGroupType.Triple;
                }
            }
            if (keyValuePairs.Count == 2)
            {
                var tmpList = keyValuePairs.OrderByDescending(x => x.Value).ToList();
                // 全单张 只有王炸
                if (keyValuePairs.Count(x => x.Value == 1) == keyValuePairs.Count)
                {
                    if ((SuitCards.First(x => x.Point == 54).Value.Contains(keyValuePairs.ElementAt(0).Key) ||
                        SuitCards.First(x => x.Point == 53).Value.Contains(keyValuePairs.ElementAt(0).Key))
                        &&
                        (SuitCards.First(x => x.Point == 54).Value.Contains(keyValuePairs.ElementAt(1).Key) ||
                        SuitCards.First(x => x.Point == 53).Value.Contains(keyValuePairs.ElementAt(1).Key)))
                    {
                        return CardGroupType.Boom;
                    }
                }
                // 连对
                if (keyValuePairs.Count(x => x.Value == 2) == keyValuePairs.Count)
                {

                }
                foreach (var tmp in tmpList)
                {
                    if (tmp.Value == 1)
                    {
                        if (SuitCards.First(x => x.Point == 54).Value.Contains(tmp.Key) ||
                            SuitCards.First(x => x.Point == 53).Value.Contains(tmp.Key))
                        {
                            return CardGroupType.Boom;
                        }
                    }
                }

                if (SuitCards.First(x => x.Point == 54).Value.Contains(keyValuePairs.ElementAt(0).Key) ||
                    SuitCards.First(x => x.Point == 54).Value.Contains(keyValuePairs.ElementAt(1).Key))
                {
                    return CardGroupType.Boom;
                }
                if (keyValuePairs.Any(x => x.Value == 3))
                {
                    if (cardGroup.Length == 4)
                    {
                        return CardGroupType.TripleWithOne;
                    }
                    else
                    {
                        return CardGroupType.TripleWithTwo;
                    }
                }
            }
            return cardGroupType;
        }

    }
}
