using CommandSims.Core;
using CommandSims.Modules.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Archive
{
    public class ArchiveWorldData
    {
        public string Weather { get; set; }

        public DateTime WorldTime { get; set; }

        public List<ActiveNpc> ActiveNpcs { get; set; }

        public ArchiveWorldData()
        {
            ActiveNpcs = new List<ActiveNpc>();
        }

    }
}
