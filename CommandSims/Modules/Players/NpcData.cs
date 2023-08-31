using CommandSims.Core;
using CommandSims.Entity.Npc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Npc
{
    public class NpcData
    {
        public static List<Player> Players = new List<Player>();

        public void InitPlayerData()
        {
            Players.Clear();

        }

        public void LoadDefaultData()
        {
            Players.Clear();

            Sims.World.AddNpc();
            Players.Add(new Player()
            {

            });

        }
    }
}
