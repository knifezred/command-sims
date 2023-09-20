using CommandSims.Core;
using CommandSims.Enums;
using CommandSims.Modules.PokerCards;
using CommandSims.Modules.Seeds;
using CommandSims.Utils;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommandSims.Stories
{
    /// <summary>
    /// 序章 创建角色，基础属性设置
    /// </summary>
    public class S0_SomeoneBorned
    {
        public void PlayerBorn()
        {
            Sims.World.CreateNewWorld(0);
            UI.ChapterTitle("序章");
            Task.Delay(100).Wait();
            var name = UI.SetPlayerName();
            var race = UI.EnumSelect<RaceEnum>();
            var gender = UI.EnumSelect<GenderEnum>();
            Sims.Context.Player = new Entity.Npc.Player
            {
                Id = 0,
                Exp = 0,
                HP = 100,
                MP = 100,
                Name = name,
                Gender = gender,
                Race = race,
                Age = 0,
            };
            UI.LoadEvent("天赋选择");
            ChildhoodEvents();
            UI.PlayerInfoPanel();
        }

        public void ChildhoodEvents()
        {
            UI.ChapterTitle("童年");
            // 出生前 自定义性别姓名种族
            var msg = string.Format("{0},{1},你出生了，是个{2}孩", Sims.World.GetWorldTime(), Sims.Weather.Value, Sims.Context.Player.Gender.GetEnumDisplayName());
            UI.PrintLine(msg);
            UI.LoadEvent("家境");
            Sims.World.UpdateWorldTime(365);
            UI.LoadEvent("抓周");
        }

        /// <summary>
        /// 打扑克
        /// </summary>
        public void PokerGame()
        {
            var pokerEngine = new PokerEngine();
            pokerEngine.PlayGame();
        }

    }
}
