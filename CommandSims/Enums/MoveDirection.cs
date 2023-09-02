using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    [Description("移动方位")]
    public enum MoveDirection
    {
        [Display(Name = "进入")]
        Enter,
        [Display(Name = "西南")]
        LeftDown,
        [Display(Name = "南")]
        Down,
        [Display(Name = "东南")]
        RightDown,
        [Display(Name = "西")]
        Left,
        [Display(Name = "留在此地")]
        Center,
        [Display(Name = "东")]
        Right,
        [Display(Name = "西北")]
        LeftUp,
        [Display(Name = "北")]
        Up,
        [Display(Name = "东北")]
        RightUp,
        [Display(Name = "退出")]
        Exit,
    }
}
