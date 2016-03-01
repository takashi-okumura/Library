using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>SQL Server用のオブジェクトのユーティリティクラス。
    /// </summary>
    [DebuggerStepThrough]
    public static class SqlServierUtility
    {
        /// <summary>最後に採番されたオートナンバーを返す。
        /// </summary>
        /// <param name="activeConnection"></param>
        /// <returns></returns>
        public static int GetLastAutoNumber(this SqlConnection activeConnection)
        {
            return SqlServierUtility.GetLastAutoNumber(activeConnection, null);
        }

        /// <summary>最後に採番されたオートナンバーを返す。
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int GetLastAutoNumber(this SqlTransaction transaction)
        {
            return SqlServierUtility.GetLastAutoNumber((SqlConnection)transaction.Connection, transaction);
        }

        /// <summary>最後に採番されたオートナンバーを返す。
        /// </summary>
        /// <param name="activeConnection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static int GetLastAutoNumber(this SqlConnection activeConnection, SqlTransaction transaction)
        {
            int lastAutoNumber = 0;
            using (var cmd = new SqlCommand("SELECT @@IDENTITY", activeConnection, transaction))
            {
                lastAutoNumber = (int)cmd.ExecuteScalar();
            }
            return lastAutoNumber;
        }

        /// <summary>SqlParameterCollection に連番のパラメタとその値を設定する。
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="parameternamePrefix"></param>
        /// <param name="values"></param>
        public static void AddValue(this SqlParameterCollection parameters, string parameternamePrefix, IEnumerable<int> values)
        {
            int i = -1;
            foreach (var value in values)
            {
                parameters.AddValue(parameternamePrefix + (++i), value);
            }
        }

        /// <summary>値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="parameterName">パラメーターの名前。</param>
        /// <param name="value">パラメーターの値。</param>
        public static void AddValue(this SqlParameterCollection parameters, string parameterName, int value)
        {
            parameters.Add(parameterName, SqlDbType.Int).Value = value;
        }

        /// <summary>値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="parameterName">パラメーターの名前。</param>
        /// <param name="value">パラメーターの値。</param>
        public static void AddValue(this SqlParameterCollection parameters, string parameterName, int? value)
        {
            parameters.Add(parameterName, SqlDbType.Int).Value = value.HasValue ? (object)value.Value : DBNull.Value;
        }

        /// <summary>値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="parameterName">パラメーターの名前。</param>
        /// <param name="value">パラメーターの値。</param>
        public static void AddValue(this SqlParameterCollection parameters, string parameterName, bool value)
        {
            parameters.Add(parameterName, SqlDbType.Bit).Value = value ? 1 : 0;
        }

        /// <summary>値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="parameterName">パラメーターの名前。</param>
        /// <param name="value">パラメーターの値。</param>
        public static void AddValue(this SqlParameterCollection parameters, string parameterName, string value)
        {
            object val = DBNull.Value;
            int    len = 0;
            if (value != null)
            {
                val = value;
                len = value.Length;
            }

            parameters.Add(parameterName, SqlDbType.VarChar, len).Value = val;
        }

        /// <summary>値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="parameterName">パラメーターの名前。</param>
        /// <param name="value">パラメーターの値。</param>
        public static void AddValue(this SqlParameterCollection parameters, string parameterName, decimal value)
        {
            parameters.Add(parameterName, SqlDbType.Decimal).Value = value;
        }

        /// <summary>値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="parameterName">パラメーターの名前。</param>
        /// <param name="value">パラメーターの値。</param>
        public static void AddValue(this SqlParameterCollection parameters, string parameterName, decimal? value)
        {
            parameters.Add(parameterName, SqlDbType.Decimal).Value = value.HasValue ? (object)value.Value : DBNull.Value;
        }

        /// <summary>値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="parameterName">パラメーターの名前。</param>
        /// <param name="value">パラメーターの値。</param>
        public static void AddValue(this SqlParameterCollection parameters, string parameterName, DateTime value)
        {
            parameters.Add(parameterName, SqlDbType.DateTime).Value = value;
        }

        /// <summary>SQLのIN演算子の要素となるパラメーターを作成する。prefixに「@model」、countに5を与えると、「@model0, @model1, @model2, @model3, @model4」という文字列がが返る。
        /// </summary>
        /// <param name="prefix">パラメーター文字列のプレフィクス。</param>
        /// <param name="count">パラメーターの数。</param>
        /// <returns>パラメーターをカンマで繋げた文字列。</returns>
        public static string CreateSqlInParameters(string prefix, int count)
        {
            return string.Join(", ", Enumerable.Range(0, count).Select(idx => prefix + idx));
        }

        /// <summary>指定したプリフィクスと0から始まるインデックス番号で構成されるパラメーターに値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="prefix">パラメーターのプリフィクス。</param>
        /// <param name="values">値のコレクション。</param>
        public static void AddValues(this SqlParameterCollection parameters, string prefix, IEnumerable<string> values)
        {
            int i = -1;
            foreach (string value in values)
            {
                parameters.AddValue(prefix + (++i), value);
            }
        }

        /// <summary>指定したプリフィクスと0から始まるインデックス番号で構成されるパラメーターに値を追加する。
        /// </summary>
        /// <param name="parameters">パラメーターのコレクション。</param>
        /// <param name="prefix">パラメーターのプリフィクス。</param>
        /// <param name="values">値のコレクション。</param>
        public static void AddValues(this SqlParameterCollection parameters, string prefix, IEnumerable<decimal> values)
        {
            int i = -1;
            foreach (decimal value in values)
            {
                parameters.AddValue(prefix + (++i), value);
            }
        }
    }
}
