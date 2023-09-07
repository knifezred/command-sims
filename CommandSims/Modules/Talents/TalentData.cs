using CommandSims.Core;
using CommandSims.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Talents
{
    public class TalentData
    {
        public List<Talent> Talents { get; set; }


        public TalentData()
        {
            Talents = new List<Talent>();
        }
        public void LoadTalentPool()
        {
            Talents.Clear();
            Talents.Add(new Talent()
            {
                Id = 1,
                Name = "",
                Description = "",
                Effects = Sims.Game.GetEffects(1, 2, 3),
                ExclusionTalents = new List<int> { }
            });
        }
    }
}
