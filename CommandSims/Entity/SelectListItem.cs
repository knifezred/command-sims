using System;
using System.Collections.Generic;
using System.Text;

namespace CommandSims.Entity.Base
{
    /// <summary>
    /// 下拉菜单项
    /// </summary>
    public class ComboSelectListItem : SimpleListItem
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        /// <value></value>
        public string Icon { get; set; }

    }

    /// <summary>
    /// 树形下拉菜单项
    /// </summary>
    public class TreeSelectListItem : SimpleTreeItem
    {
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool Expended { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 是否子节点
        /// </summary>
        public bool Leaf => Children == null || Children.Count == 0;

        public List<TreeSelectListItem> Children { get; set; }
    }
}
