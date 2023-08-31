using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Entity.Npc
{
    /// <summary>
    /// 天赋
    /// </summary>
    public class PlayerTalent
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<TalentEffect> Effects { get; set; }

    }
}
