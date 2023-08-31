using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSims.Entity.Base
{

    /// <summary>
    /// 简单键值对类，用来存著生成下拉菜单的数据
    /// </summary>
    public class SimpleListItem
    {
        public object Text { get; set; }
        public object Value { get; set; }
    }
}
