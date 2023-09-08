using CommandSims.Entity;
using CommandSims.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Events
{
    public class EventEntity : RandomBase
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

        public int MaxSelect { get; set; }

        public List<EffectEntity> Effects { get; set; }

        public EventEntity()
        {
            this.Id = 0;
            this.Weight = 10;
            this.Effects = new List<EffectEntity>();
            this.Selects = new List<EventSelectItem>();
            this.Includes = new List<int>();
            this.Excludes = new List<int>();
            this.Enabled = true;
            this.MaxSelect = 1;
        }


    }
}
