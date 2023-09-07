using CommandSims.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Talents
{
    /// <summary>
    /// 天赋
    /// </summary>
    public class Talent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<EffectEntity> Effects { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsAcquired { get; set; }
        /// <summary>
        /// 互斥
        /// </summary>
        public List<int> ExclusionTalents { get; set; }


    }
}
