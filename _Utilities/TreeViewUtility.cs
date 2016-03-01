using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Library
{
    /// <summary>TreeView のユーティリティクラス。
    /// </summary>
    [DebuggerStepThrough]
    public static class TreeViewUtility
    {
        /// <summary>子孫ノードを取得する。
        /// </summary>
        /// <param name="node">ツリーノードオブジェクト。</param>
        /// <returns>子孫ノードの配列。</returns>
        public static TreeNode[] GetDescendants(this TreeNode node)
        {
            var list = new List<TreeNode>(TreeViewUtility.GetDescendants(node.Nodes));
            list.Insert(0, node);
            return list.ToArray();
        }

        /// <summary>子孫ノードを取得する。
        /// </summary>
        /// <param name="nodes">ツリーノードのコレクション。</param>
        /// <returns>子孫ノードの配列。</returns>
        public static TreeNode[] GetDescendants(this TreeNodeCollection nodes)
        {
            var list = new List<TreeNode>();
            TreeViewUtility.GetDescendants(nodes, list);
            return list.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="list"></param>
        private static void GetDescendants(this TreeNodeCollection nodes, List<TreeNode> list)
        {
            foreach (TreeNode node in nodes)
            {
                list.Add(node);
                GetDescendants(node.Nodes, list);
            }
        }

        /// <summary>兄弟ノードを取得する。
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TreeNode[] GetBrotherNodes(this TreeNode node)
        {
            var broNodes = new List<TreeNode>();

            for (var iNode = node.PrevNode; iNode != null; iNode = iNode.PrevNode)
            {
                broNodes.Add(iNode);
            }

            broNodes.Reverse();

            for (var iNode = node.NextNode; iNode != null; iNode = iNode.NextNode)
            {
                broNodes.Add(iNode);
            }

            return broNodes.ToArray();
        }

        /// <summary>ルートノードからのパスを表すノード配列を取得する。
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TreeNode[] GetPath(this TreeNode node)
        {
            var list = new List<TreeNode>();
            for (var i = node; i != null; i = i.Parent)
            {
                list.Add(i);
            }

            return list.Reverse<TreeNode>().ToArray();
        }

        /// <summary>子ノードを列挙する。
        /// </summary>
        /// <param name="treeView"></param>
        /// <returns></returns>
        public static IEnumerable<TreeNode> EnumerateNodes(this TreeView treeView)
        {
            return treeView.Nodes.Cast<TreeNode>();
        }

        /// <summary>ノードを列挙する。
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static IEnumerable<TreeNode> EnumerateNodes(this TreeNode node)
        {
            return node.Nodes.Cast<TreeNode>();
        }
    }
}
