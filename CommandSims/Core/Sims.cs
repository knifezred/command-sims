﻿using CommandSims.Entity.Base;
using CommandSims.Entity.Npc;
using CommandSims.Modules.Archive;
using CommandSims.Modules.Maps;
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

        public static void StartInit()
        {
            Context = new ArchiveContext
            {
                Player = new Player(),
                BagItems = new List<ArchiveItem>(),
                StorageItems = new List<ArchiveItem>(),
                WorldData = new ArchiveWorldData()
            };
            Game = new GameFramework();
            World = new WorldFrame();
        }

        public static void Reload(ArchiveContext archiveContext)
        {
            Context = archiveContext;
            #region 修复老存档空数据问题
            if (Sims.Context.WorldData == null)
            {
                Sims.Context.WorldData = new ArchiveWorldData();
            }
            #endregion
            World = new WorldFrame();
            Game = new GameFramework();
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
