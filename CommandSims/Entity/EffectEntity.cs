using CommandSims.Entity.Base;
using CommandSims.Enums;
using CommandSims.Modules.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Entity
{
    public class EffectEntity
    {
        public int Id { get; set; }

        public EffectEnum Type { get; set; }

        public PlayerAttribute Attribute { get; set; }

        public List<ItemBase> Items { get; set; }

    }
}
