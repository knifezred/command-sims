using CommandSims.Entity;
using CommandSims.Entity.Base;
using CommandSims.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Events
{
    public class EventSelectItem : SimpleListItem
    {
        public int TalentId { get; set; }

        public string EventName { get; set; }

        public GradeEnum Grade { get; set; }

        public List<EffectEntity> Effects { get; set; }

        public bool Disabled { get; set; }

    }
}
