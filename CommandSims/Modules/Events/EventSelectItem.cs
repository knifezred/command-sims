using CommandSims.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Events
{
    public class EventSelectItem : SimpleListItem
    {
        public int EventId { get; set; }

        public List<string> Effects { get; set; }

        public bool Disabled { get; set; }
    }
}
