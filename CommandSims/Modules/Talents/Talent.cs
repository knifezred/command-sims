using CommandSims.Entity;
using CommandSims.Entity.Base;
using CommandSims.Enums;
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
    public class Talent : RandomBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GradeEnum Grade { get; set; }

        public List<EffectEntity> Effects { get; set; }

        public bool IsEnabled { get; set; }
        /// <summary>
        /// 是否后天天赋
        /// </summary>
        public bool IsAcquired { get; set; }
        /// <summary>
        /// 互斥
        /// </summary>
        public List<int> ExclusionTalents { get; set; }

        public Talent()
        {
            this.Id = 0;
            this.Weight = 10;
            this.Effects = new List<EffectEntity>();
            this.IsEnabled = true;
            this.IsAcquired = false;
            this.Grade = GradeEnum.Silver;
        }


    }
}
