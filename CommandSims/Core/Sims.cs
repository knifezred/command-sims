using CommandSims.Entity;
using CommandSims.Entity.Base;
using CommandSims.Entity.Npc;
using CommandSims.Modules.Archive;
using CommandSims.Modules.Maps;
using CommandSims.Modules.Seeds;
using CommandSims.Service;

namespace CommandSims.Core
{
    public class Sims
    {
        public static GameFramework Game { get; set; }

        public static ArchiveContext Context { get; set; }

        public static ArchiveWorldData WorldData => Context.WorldData;

        public static WorldFrame World { get; set; }

        public static DateTime WorldTime => World.GetWorldTime();

        public static SimpleListItem Weather => World.GetWorldWeather();

        public static SeedsData Seeds { get; set; }


        public static void StartInit()
        {
            // 加载种子数据
            Seeds = new SeedsData();
            // 加载游戏框架
            Game = new GameFramework();
            Game.AfterLoad();
            // 创建空存档
            Context = new ArchiveContext
            {
                Player = new Player(),
                BagItems = new List<ArchiveItem>(),
                StorageItems = new List<ArchiveItem>(),
                WorldData = new ArchiveWorldData()
            };
            // 加载世界数据
            World = new WorldFrame();

        }

        public static void Reload(ArchiveContext archiveContext)
        {
            Context = archiveContext;
            #region 修复老存档空数据问题
            //if (Sims.Context.WorldData == null)
            //{
            //    Sims.Context.WorldData = new ArchiveWorldData();
            //}
            #endregion
            World = new WorldFrame();
        }

        public static Player? GetPlayer(int playerId)
        {
            var player = Sims.Context.Player;
            if (playerId > 0)
            {
                player = Sims.World.NpcList.FirstOrDefault(x => x.Id == playerId);
            }
            return player;
        }

    }
}
