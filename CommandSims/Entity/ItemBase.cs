using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Entity
{
    public class ItemBase
    {

        [Description("编号")]
        public int Id { get; set; }

        [Description("名称")]
        public string Name { get; set; }

        [Description("描述")]
        public string Description { get; set; }

        [Description("分类")]
        public string Category { get; set; }

        [Description("类型")]
        public string Type { get; set; }

        [Description("等级")]
        public string Level { get; set; }

        [Description("重量")]
        public double Weight { get; set; }



    }
}
