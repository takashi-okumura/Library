using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>IEnumerable インターフェイスのユーティリティクラス。
    /// </summary>
    [DebuggerStepThrough]
    public static class EnumerableUtility
    {
        /// <summary>コレクションの各アイテムに対して指定したメソッドを実行する。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <param name="collection">コレクション。</param>
        /// <param name="action">実行するメソッド。</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        /// <summary>コレクションの各アイテムに対して指定したメソッドを実行する。0から始まるインデックス番号を利用することができる。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <param name="collection">コレクション。</param>
        /// <param name="action">実行するメソッド。</param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<int, T> action)
        {
            int i = -1;
            foreach (var item in collection)
            {
                action(++i, item);
            }
        }

        /// <summary>指定されたアイテムがコレクションの最後に登録さているかどうかを判断します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool ContainsAtLast<T>(this IEnumerable<T> collection, T item)
        {
            return collection.LastOrDefault().Equals(item);
        }

        /// <summary>シーケンスの最初の要素を返す。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <param name="collection">返される要素を含むコレクション。</param>
        /// <param name="defaultValue">コレクションが空である場合に返される値。</param>
        /// <returns>コレクションの最初の要素。または指定した規定値。</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, T defaultValue)
        {
            foreach (var item in collection)
            {
                return item;
            }

            return defaultValue;
        }

        /// <summary>条件を満たすシーケンスの最初の要素を返す。
        /// </summary>
        /// <typeparam name="T">コレクションの要素の型。</typeparam>
        /// <param name="collection">返される要素を含むコレクション。</param>
        /// <param name="predicate">各要素が条件を満たしているかどうかをテストする関数。</param>
        /// <param name="defaultValue">コレクションに条件を満たす要素が含まれていない場合に返される値。</param>
        /// <returns>条件を満たす最初の要素。または指定された規定値。</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> collection, Func<T, bool> predicate, T defaultValue)
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return defaultValue;
        }

        /// <summary>指定されたキーセレクター関数によってグルーピングした結果を List&lt;T&gt; を値に持つ Dictionary&lt;TKey, TValue&gt; に変換します。
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="collection"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static Dictionary<TKey, List<TElement>> ToGroupDictionary<TKey, TElement>(this IEnumerable<TElement> collection, Func<TElement, TKey> keySelector)
        {
            return EnumerableUtility.ToGroupDictionary(collection.GroupBy(keySelector));
        }

        /// <summary>List&lt;T&gt; を値に持つ Dictionary&lt;TKey, TValue&gt; に変換します。
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="groups"></param>
        /// <returns></returns>
        public static Dictionary<TKey, List<TElement>> ToGroupDictionary<TKey, TElement>(this IEnumerable<IGrouping<TKey, TElement>> groups)
        {
            return groups.ToDictionary(group => group.Key, group => group.ToList());
        }
    }
}
