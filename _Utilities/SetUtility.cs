using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>ISet&lt;T&gt;のユーティリティクラス。
    /// </summary>
    public static class SetUtility
    {
        /// <summary>指定したコレクションの要素を登録する。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="set"></param>
        /// <param name="collection"></param>
        public static void AddRange<T>(this ISet<T> set, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                set.Add(item);
            }
        }
    }
}
