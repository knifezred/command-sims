using CommandSims.Core;
using CommandSims.Modules.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommandSims.Entity
{
    public class TreeNode<T>
    {
        public T Node { get; set; }

        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T node)
        {
            Node = node;
            Children = new List<TreeNode<T>>();
        }

        public void AddChild(T node)
        {
            Children.Add(new TreeNode<T>(node));
        }

        public void AddChildren(IEnumerable<T> children)
        {
            foreach (var child in children)
            {
                Children.Add(new TreeNode<T>(child));
            }
        }


        private static int autoId;
        public void ResetAutoId()
        {
            autoId = 0;
        }
        /// <summary>
        /// 遍历Tree
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        public void TraverseWithAutoId(TreeNode<MapEntity> node, Action<MapEntity> action)
        {
            node.Node.Id = autoId++;
            action(node.Node);
            foreach (var child in node.Children)
            {
                child.Node.ParentId = node.Node.Id;
                TraverseWithAutoId(child, action);
            }
        }


        /// <summary>
        /// 遍历Tree
        /// </summary>
        /// <param name="node"></param>
        /// <param name="action"></param>
        public void Traverse(TreeNode<MapEntity> node, Action<MapEntity> action)
        {
            action(node.Node);
            foreach (var child in node.Children)
            {
                Traverse(child, action);
            }
        }

    }
}
