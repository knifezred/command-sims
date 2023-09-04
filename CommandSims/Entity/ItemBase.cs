using CommandSims.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Entity.Base
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
        public ItemType Type { get; set; }

        [Description("等级")]
        public GradeEnum Level { get; set; }

        [Description("重量")]
        public double Weight { get; set; }

        [Description("价格")]
        public int Price { get; set; }


        public void Use()
        {

        }

        public void Unuse()
        {

        }
    }
}
