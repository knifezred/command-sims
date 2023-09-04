using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.PokerCards
{
    [Description("叫地主")]
    public enum LandloarActionEnum
    {
        [Display(Name = "不要")]
        pass,
        [Display(Name = "抢地主")]
        call,
        [Display(Name = "加倍")]
        doubleCall,
        [Display(Name = "超级加倍")]
        tripleCall,

    }
}
