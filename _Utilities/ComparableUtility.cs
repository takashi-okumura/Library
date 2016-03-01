using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>IComaprable のユーティリティクラス。
    /// </summary>
    public static class ComparableUtility
    {
        /// <summary>a 以上かつ b 以下であるかどうかを判断する。
        /// </summary>
        /// <typeparam name="T">IComparable を持つ型。</typeparam>
        /// <param name="comparable">検査対象。</param>
        /// <param name="a">最小の値。</param>
        /// <param name="b">最大の値。</param>
        /// <returns>範囲内であればtrue。</returns>
        public static bool Between<T>(this T comparable, T a, T b) where T : IComparable
        {
            return (a.CompareTo(comparable) <= 0) && (comparable.CompareTo(b) <= 0);
        }
    }
}
