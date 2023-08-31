using CommandSims.Entity.Archive;
using CommandSims.Entity.Npc;
using CommandSims.Service;

namespace CommandSims.Core
{
    public class Sims
    {

        public static GameFramework Game { get; set; }

        public static WorldFramework World { get; set; }

        public static ArchiveData PlayerData { get; set; }

        public static List<Player> NpcList { get; set; }

        public static void StartInit()
        {
            Game = new GameFramework();
            World = new WorldFramework();
            PlayerData = new ArchiveData
            {
                PlayerInfo = new Player(),
                BagItems = new List<ArchiveItem>(),
                StorageItems = new List<ArchiveItem>()
            };
        }

        public static Player? GetPlayer(int playerId)
        {
            var player = Sims.PlayerData.PlayerInfo;
            if (playerId > 0)
            {
                //npc
                player = Sims.NpcList.FirstOrDefault(x => x.Id == playerId);
            }
            return player;
        }

    }
}
