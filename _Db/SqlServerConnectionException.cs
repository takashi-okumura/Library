using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    /// <summary>SQL Serverに接続できない場合に発生するエラー。
    /// </summary>
    public class SqlServerConnectionException : Exception
    {
        /// <summary>初期化する。
        /// </summary>
        /// <param name="connectionString">接続文字列</param>
        public SqlServerConnectionException(string connectionString) : base(string.Format("データベースに接続できません。\r\n接続文字列:「{0}」", connectionString))
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>接続文字列。
        /// </summary>
        public string ConnectionString { get; private set; }
    }
}
