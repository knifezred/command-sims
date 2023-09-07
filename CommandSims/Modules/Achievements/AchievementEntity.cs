using CommandSims.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Achievements
{
    /// <summary>
    /// 成就
    /// </summary>
    public class AchievementEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public GradeEnum Grade { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public string Condition { get; set; }
        /// <summary>
        /// 触发时机
        /// </summary>
        public string Opportunity { get; set; }

    }
}
