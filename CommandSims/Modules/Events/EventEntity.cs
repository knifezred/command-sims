using CommandSims.Entity;
using CommandSims.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Events
{
    public class EventEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<int> Includes { get; set; }

        public List<int> Excludes { get; set; }

        public bool Enabled { get; set; }

        /// <summary>
        /// 分支选择
        /// </summary>
        public List<EventSelectItem> Selects { get; set; }

        public List<EffectEntity> Effects { get; set; }



    }
}
