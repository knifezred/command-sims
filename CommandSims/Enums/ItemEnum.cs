using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Enums
{
    public enum ItemEnum
    {
        [Description("未分类")]
        Uncategorized = 0,
        [Description("武器")]
        Weapon = 1,
        [Description("装备")]
        Equipment = 2,
        [Description("药品")]
        Drug = 3,
        [Description("书籍")]
        Book = 4,
        [Description("食物")]
        Food = 5,
        [Description("材料")]
        Materials = 6,
        [Description("任务物品")]
        TaskItem = 7,
        [Description("杂物")]
        Sundries = 8




    }
}
