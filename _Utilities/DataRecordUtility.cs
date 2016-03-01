using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>
    ///
    /// </summary>
    [DebuggerStepThrough]
    public static class DataRecordUtility
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool IsDBNull(this IDataRecord dataRecord, string columnName)
        {
            return dataRecord.IsDBNull(dataRecord.GetOrdinal(columnName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static int GetInt32(this IDataRecord dataRecord, string name)
        {
            try
            {
                return dataRecord.GetInt32(dataRecord.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static int? GetInt32OrNull(this IDataRecord dataRecord, string name)
        {
            try
            {
                int ordinal = dataRecord.GetOrdinal(name);
                int? value = null;
                if (!dataRecord.IsDBNull(ordinal))
                {
                    value = dataRecord.GetInt32(ordinal);
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static long GetInt64(this IDataRecord dataRecord, string name)
        {
            try
            {
                return dataRecord.GetInt64(dataRecord.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static long? GetInt64OrNull(this IDataRecord dataRecord, string name)
        {
            try
            {
                int ordinal = dataRecord.GetOrdinal(name);
                long? value = null;
                if (!dataRecord.IsDBNull(ordinal))
                {
                    value = dataRecord.GetInt64(ordinal);
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static bool GetBoolean(this IDataRecord dataRecord, string name)
        {
            try
            {
                return dataRecord.GetBoolean(dataRecord.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static bool? GetBooleanOrNull(this IDataRecord dataRecord, string name)
        {
            try
            {
                int ordinal = dataRecord.GetOrdinal(name);
                bool? value = null;
                if (!dataRecord.IsDBNull(ordinal))
                {
                    value = dataRecord.GetBoolean(ordinal);
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static DateTime GetDateTime(this IDataRecord dataRecord, string name)
        {
            try
            {
                return dataRecord.GetDateTime(dataRecord.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static DateTime? GetDateTimeOrNull(this IDataRecord dataRecord, string name)
        {
            try
            {
                int ordinal = dataRecord.GetOrdinal(name);
                DateTime? value = null;
                if (!dataRecord.IsDBNull(ordinal))
                {
                    value = dataRecord.GetDateTime(ordinal);
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static double GetDouble(this IDataRecord dataRecord, string name)
        {
            try
            {
                return dataRecord.GetDouble(dataRecord.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static double? GetDoubleOrNull(this IDataRecord dataRecord, string name)
        {
            try
            {
                int ordinal = dataRecord.GetOrdinal(name);
                double? value = null;
                if (!dataRecord.IsDBNull(ordinal))
                {
                    value = dataRecord.GetDouble(ordinal);
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static decimal GetDecimal(this IDataRecord dataRecord, string name)
        {
            try
            {
                return dataRecord.GetDecimal(dataRecord.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static decimal? GetDecimalOrNull(this IDataRecord dataRecord, string name)
        {
            try
            {
                int ordinal = dataRecord.GetOrdinal(name);
                decimal? value = null;
                if (!dataRecord.IsDBNull(ordinal))
                {
                    value = dataRecord.GetDecimal(ordinal);
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRecord">レコード</param>
        /// <param name="name">フィールド名</param>
        /// <returns>指定したフィールドの値</returns>
        public static string GetString(this IDataRecord dataRecord, string name)
        {
            try
            {
                return dataRecord.GetString(dataRecord.GetOrdinal(name));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="dataRecord"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetStringOrNull(this IDataRecord dataRecord, string name)
        {
            try
            {
                int ordinal = dataRecord.GetOrdinal(name);
                string value = null;
                if (!dataRecord.IsDBNull(ordinal))
                {
                    value = dataRecord.GetString(ordinal);
                }
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ":" + name, ex);
            }
        }
    }
}
