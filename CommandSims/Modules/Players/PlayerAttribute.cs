using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Players
{
    /// <summary>
    /// SPECIAL属性
    /// </summary>
    public class PlayerAttribute
    {

        /// <summary>
        /// 力量
        /// </summary>
        [Description("力量")]
        public int Strength { get; set; }

        /// <summary>
        /// 感知
        /// </summary>
        [Description("感知")]
        public int Perception { get; set; }

        /// <summary>
        /// 耐力
        /// </summary>
        [Description("耐力")]
        public int Endurance { get; set; }

        /// <summary>
        /// 魅力
        /// </summary>
        [Description("魅力")]
        public int Charisma { get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        [Description("智力")]
        public int Intelligence { get; set; }

        /// <summary>
        /// 敏捷
        /// </summary>
        [Description("敏捷")]
        public int Agility { get; set; }

        /// <summary>
        /// 幸运
        /// </summary>
        [Description("幸运")]
        public int Lucky { get; set; }

        public PlayerAttribute(int strength = 0, int perception = 0, int endurance = 0, int charisma = 0, int intelligence = 0, int agility = 0, int lucky = 0)
        {
            this.Strength = strength;
            this.Perception = perception;
            this.Endurance = endurance;
            this.Charisma = charisma;
            this.Intelligence = intelligence;
            this.Agility = agility;
            this.Lucky = lucky;

        }

    }
}
