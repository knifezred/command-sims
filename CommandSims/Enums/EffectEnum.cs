using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    public enum EffectEnum
    {
        [Display(Name = "属性")]
        Attribute = 1,
        [Display(Name = "天赋")]
        Talent,
        [Display(Name = "物品")]
        Item, 
        [Display(Name = "能力")]
        Ability



    }
}
