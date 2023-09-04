using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    [Description("物品分类")]
    public enum ItemType
    {
        [Display(Name = "未分类")]
        Uncategorized = 0,
        [Display(Name = "武器")]
        Weapon = 1,
        [Display(Name = "装备")]
        Equipment = 2,
        [Display(Name = "药品")]
        Drug = 3,
        [Display(Name = "书籍")]
        Book = 4,
        [Display(Name = "食物")]
        Food = 5,
        [Display(Name = "材料")]
        Materials = 6,
        [Display(Name = "任务物品")]
        TaskItem = 7,
        [Display(Name = "杂物")]
        Sundries = 8




    }
}
