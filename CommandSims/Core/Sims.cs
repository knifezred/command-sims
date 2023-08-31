using CommandSims.Entity.Base;
using CommandSims.Entity.Npc;
using CommandSims.Modules.Archive;
using CommandSims.Modules.Maps;
using CommandSims.Service;

namespace CommandSims.Core
{
    public class Sims
    {
        public static ArchiveData Context { get; set; }

        public static GameFramework Game { get; set; }

        public static WorldFrame World { get; set; }

        public static DateTime WorldTime => World.GetWorldTime();

        public static SimpleListItem Weather => World.GetWorldWeather();

        public static void StartInit()
        {
            Game = new GameFramework();
            World = new WorldFrame();
            Context = new ArchiveData
            {
                PlayerInfo = new Player(),
                BagItems = new List<ArchiveItem>(),
                StorageItems = new List<ArchiveItem>(),
                WorldData = new ArchiveWorldData()
            };
        }

        public static Player? GetPlayer(int playerId)
        {
            var player = Sims.Context.PlayerInfo;
            if (playerId > 0)
            {
                player = Sims.World.NpcList.FirstOrDefault(x => x.Id == playerId);
            }
            return player;
        }

    }
}
