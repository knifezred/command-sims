using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSims.Entity.Base
{
    /// <summary>
    /// 简单树形结构类
    /// </summary>
    public class SimpleTreeItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 显示字段
        /// </summary>
        public string Text { get; set; }
    }
}
