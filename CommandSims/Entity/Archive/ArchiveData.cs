using CommandSims.Entity.Npc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Entity.Archive
{
    /// <summary>
    /// 存档数据
    /// </summary>
    public class ArchiveData
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime WorldTime { get; set; }

        public int Money { get; set; }

        public Player PlayerInfo { get; set; }

        public List<ArchiveItem> BagItems { get; set; }

        public List<ArchiveItem> StorageItems { get; set; }

        public object WorldData { get; set; }

    }
}
