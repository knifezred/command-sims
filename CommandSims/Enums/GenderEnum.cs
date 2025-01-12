﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    /// <summary>
    /// 性别
    /// </summary>
    [Description("性别")]
    public enum GenderEnum
    {
        [Display(Name = "男", Description = "")]
        Male = 1,
        [Display(Name = "女")]
        Female = 2,
        [Display(Name = "其他")]
        Other = 3
    }
}
