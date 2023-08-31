using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSims.Entity.Base;

namespace CommandSims.Entity
{
    public class TalentEffect : RandomBase
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Type { get; set; }

        public int DefaultValue { get; set; }
    }
}
