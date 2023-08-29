using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("人族")]
        Human = 1,
        [Description("高精灵")]
        HighElf,
        [Description("兽族")]
        Orc,
    }
}
