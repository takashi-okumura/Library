using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>文字列用のユーティリティクラス。
    /// </summary>
    [DebuggerStepThrough]
    public static class StringUtility
    {
        /// <summary>フォーマットを指定して文字列を追加する。
        /// </summary>
        /// <param name="stringList">文字列のリスト</param>
        /// <param name="format">フォーマット</param>
        /// <param name="args">引数</param>
        public static void Add(this List<string> stringList, string format, params object[] args)
        {
            stringList.Add(string.Format(format, args));
        }

        /// <summary>区切り文字で区切られた部分文字列を格納した配列を取得する。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string[] Split(this string str, char separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>文字列を解析して日時に変換する。
        /// </summary>
        /// <param name="str">解析する文字列。</param>
        /// <returns>変換が成功した場合はその日時。失敗した場合はnull。</returns>
        public static DateTime? ToDateTime(this string str)
        {
            DateTime dt;
            if (DateTime.TryParse(str, out dt))
            {
                return dt;
            }
            return null;
        }

        /// <summary>文字列を解析してInt32に変換する。
        /// </summary>
        /// <param name="str">解析する文字列。</param>
        /// <returns>変換が成功した場合はその値。失敗した場合はnull。</returns>
        public static int? ToInt32(this string str)
        {
            int val;
            if (int.TryParse(str, out val))
            {
                return val;
            }
            return null;
        }

        /// <summary>文字列を解析してDecimalに変換する。
        /// </summary>
        /// <param name="str">解析する文字列。</param>
        /// <returns>変換が成功した場合はその値。失敗した場合はnull。</returns>
        public static decimal? ToDecimal(this string str)
        {
            decimal val;
            if (decimal.TryParse(str, out val))
            {
                return val;
            }
            return null;
        }

        /// <summary>null でない場合、Trimする。
        /// </summary>
        /// <param name="str">文字列。</param>
        /// <returns>Trimした文字列。またはnull。</returns>
        public static string TrimIfNotNull(this string str)
        {
            if (str == null)
            {
                return null;
            }
            return str.Trim();
        }

        /// <summary>文字列の最初から指定した文字数の文字列を取得する。
        /// </summary>
        /// <param name="str">文字列。</param>
        /// <param name="length">取得する長さ。</param>
        /// <returns></returns>
        public static string Left(this string str, int length)
        {
            return str.Substring(0, Math.Min(length, str.Length));
        }

        /// <summary>文字列のリストに書式を指定して文字列を登録する。
        /// </summary>
        /// <param name="stringList"></param>
        /// <param name="format"></param>
        /// <param name="strings"></param>
        public static void AddFormat(this IList<string> stringList, string format, params string[] strings)
        {
            stringList.Add(string.Format(format, strings));
        }

        /// <summary>コレクションに格納された文字列を区切り文字を使って連結する。
        /// </summary>
        /// <param name="stringCollection"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinStrings(this IEnumerable<string> stringCollection, string separator)
        {
            return string.Join(separator, stringCollection);
        }
    }
}
