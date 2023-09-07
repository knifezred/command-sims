using System;
using System.Collections.Generic;
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
        public int Strength { get; set; }
        
        /// <summary>
        /// /感知
        /// </summary>
        public int Perception { get; set; }

        /// <summary>
        /// 耐力
        /// </summary>
        public int Endurance { get; set; }
        
        /// <summary>
        /// 魅力
        /// </summary>
        public int Charisma {  get; set; }

        /// <summary>
        /// 智力
        /// </summary>
        public int Intelligence { get; set; }

        /// <summary>
        /// 敏捷
        /// </summary>
        public int Agility { get; set; }

        /// <summary>
        /// 幸运
        /// </summary>
        public int Lucky { get; set; }

    }
}
