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
        DarkGray = 1,
        [Display(Name = "绿色")]
        DarkGreen, 
        [Display(Name = "蓝色")]
        DarkBlue,
        [Display(Name = "青色")]
        DarkCyan,
        [Display(Name = "紫色")]
        DarkMagenta,
        [Display(Name = "橙色")]
        DarkOrange,
        [Display(Name = "金色")]
        DarkYellow,
        [Display(Name = "红色")]
        DarkRed,
    }
}
