﻿using CommandSims.Entity.Npc;
using CommandSims.Modules.Events;
using CommandSims.Modules.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Archive
{
    /// <summary>
    /// 存档数据
    /// </summary>
    public class ArchiveContext
    {
        public string Name { get; set; }

        public DateTime SavedTime { get; set; }

        public Player Player { get; set; }

        public List<EventEntity> Events { get; set; } = new();

        public List<ArchiveItem> BagItems { get; set; } = new();

        public List<ArchiveItem> StorageItems { get; set; } = new();

        public MapEntity CurrentMap { get; set; }

        public ArchiveWorldData WorldData { get; set; }
        /// <summary>
        /// 解锁成就
        /// </summary>
        public List<int> Archievements { get; set; }

    }
}