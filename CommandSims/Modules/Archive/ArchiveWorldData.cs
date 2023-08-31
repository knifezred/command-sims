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
        public List<ActiveNpc> ActiveNpcs { get; set; }

        public ArchiveWorldData()
        {
            ActiveNpcs = new List<ActiveNpc>();
        }

    }
}
