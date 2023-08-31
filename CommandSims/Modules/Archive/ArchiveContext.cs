using CommandSims.Entity.Npc;
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

        public Player PlayerInfo { get; set; }

        public int Money { get; set; }

        public List<ArchiveItem> BagItems { get; set; }

        public List<ArchiveItem> StorageItems { get; set; }

        /// <summary>
        /// 所在地图
        /// </summary>
        public MapEntity CurrentMap { get; set; }

        public ArchiveWorldData WorldData { get; set; }
    }
}
