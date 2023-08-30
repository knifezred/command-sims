using CommandSims.Entity.Archive;
using CommandSims.Entity.Npc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Core
{
    public class Sims
    {
        public static GameFramework GameFramework { get; set; }

        public static WorldGenerator WorldGenerator { get; set; }

        public static ArchiveData PlayerData { get; set; }

        public static List<Player> NpcList { get; set; }

        public static void StartInit()
        {
            GameFramework = new GameFramework();
            WorldGenerator = new WorldGenerator();
            PlayerData = new ArchiveData
            {
                PlayerInfo = new Entity.Npc.Player(),
                BagItems = new List<ArchiveItem>(),
                StorageItems = new List<ArchiveItem>()
            };
        }
    }
}
