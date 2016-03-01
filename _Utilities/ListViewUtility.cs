using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library
{
    /// <summary>
    /// </summary>
    public static class ListViewUtility
    {
        /// <summary>
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        public static IEnumerable<ListViewItem> EnumerateItems(this ListView listView)
        {
            return listView.Items.Cast<ListViewItem>();
        }

        /// <summary>
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        public static IEnumerable<ListViewItem> EnumerateSelectedItems(this ListView listView)
        {
            return listView.SelectedItems.Cast<ListViewItem>();
        }

        /// <summary>指定されたインデックス位置のリストビューサブアイテムを取得する。未作成の場合はそのインデックス位置までのサブアイテムを作成してから返す。
        /// </summary>
        /// <param name="listViewItem"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ListViewItem.ListViewSubItem GetOrCreateSubItem(this ListViewItem listViewItem, int index)
        {
            for (int i = 0; i < index - listViewItem.SubItems.Count + 1; i++)
            {
                listViewItem.SubItems.Add(string.Empty);
            }

            return listViewItem.SubItems[index];
        }
    }
}
