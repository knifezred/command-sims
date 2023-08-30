using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    /// <summary>
    /// 种族
    /// </summary>
    public enum RaceEnum
    {
        [Display(Name = "人族")]
        Human = 1,
        [Display(Name = "高精灵")]
        HighElf,
        [Display(Name = "兽族")]
        Orc,
    }
}
