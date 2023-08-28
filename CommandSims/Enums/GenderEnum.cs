using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    public enum GenderEnum
    {
        [Description("男")]
        Male = 1,
        [Description("女")]
        Female = 2,
        [Description("其他")]
        Other = 3,
    }
}
