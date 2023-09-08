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
    /// 等级
    /// </summary>
    [Description("等级")]
    public enum GradeEnum
    {
        [Display(Name = "灰色")]
        Gray = 1,
        [Display(Name = "白色")]
        Silver,
        [Display(Name = "绿色")]
        DarkGreen,
        [Display(Name = "蓝色")]
        Navy,
        [Display(Name = "青色")]
        RoyalBlue1,
        [Display(Name = "紫色")]
        Purple,
        [Display(Name = "橙色")]
        Orange1,
        [Display(Name = "金色")]
        Yellow1,
        [Display(Name = "红色")]
        Maroon,
    }
}
