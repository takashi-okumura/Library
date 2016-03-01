using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace Library
{
    /// <summary>SQL Serverデータベースのトランザクション処理を実行するクラス。
    /// </summary>
    [DebuggerStepThrough]
    public class SqlServerTransaction : IDisposable
    {
        //コンストラクタ
        /// <summary>初期化します。
        /// </summary>
        public SqlServerTransaction(string connectionString)
        {
            this.cnn = new SqlConnection();
            this.cnnStr = connectionString;
        }

        // フィールド
        /// <summary>DBコネクション
        /// </summary>
        private readonly SqlConnection cnn;

        /// <summary>接続文字列
        /// </summary>
        private readonly string cnnStr;

        /// <summary>DBトランザクション
        /// </summary>
        private SqlTransaction trans;

        // メソッド
        /// <summary>トランザクションを開始する。
        /// </summary>
        public void Begin()
        {
            // データベースに接続する。
            try
            {
                if (!this.cnn.State.HasFlag(ConnectionState.Open))
                {
                    this.cnn.ConnectionString = this.cnnStr;
                    this.cnn.Open();
                }
            }
            catch
            {
                throw new SqlServerConnectionException(this.cnnStr);
            }

            // トランザクションを開始する。
            if (this.trans != null)
            {
                this.trans.Dispose();
                this.trans = null;
            }
            this.trans = cnn.BeginTransaction();
        }

        /// <summary>レコードを列挙する。
        /// </summary>
        /// <param name="commandBuilder">コマンドを設定するメソッド。</param>
        /// <returns>レコードを変換したオブジェクトの列挙可能なコレクション。</returns>
        public IEnumerable<IDataRecord> Select(Action<SqlCommand> commandBuilder)
        {
            using (var cmd = this.cnn.CreateCommand())
            {
                cmd.Transaction = this.trans;

                // todo：タイムアウト対応（2012/06/06）
                cmd.CommandTimeout = 300;


                commandBuilder(cmd);
#if DEBUG
                this.DebugWriteCommand(cmd);
#endif
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return reader;
                    }
                }
            }
        }

        /// <summary>ストアドプロシージャを実行する。
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="sqlCommandTimeout" ></param>
        /// <param name="parametersBuilder"></param>
        /// <param name="returnValueName"></param>
        /// <param name="returnValueType"></param>
        /// <returns></returns>
        public object ExecuteStoredProcedure(string storedProcedureName, int sqlCommandTimeout, Action<SqlParameterCollection> parametersBuilder, string returnValueName, SqlDbType returnValueType)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
            {
                throw new ArgumentException("ストアドプロシージャ名が空です。", "storedProcedureName");
            }

            using (var cmd = this.cnn.CreateCommand())
            {
                cmd.Transaction = this.trans;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = storedProcedureName;
                cmd.CommandTimeout = sqlCommandTimeout;



                cmd.Parameters.Add(returnValueName, returnValueType).Direction = ParameterDirection.ReturnValue;
                parametersBuilder(cmd.Parameters);

#if DEBUG
                this.DebugWriteCommand(cmd);
#endif

                cmd.ExecuteScalar();

                return cmd.Parameters[returnValueName].Value;
            }
        }

        /// <summary>DBを更新する。
        /// </summary>
        /// <param name="commandBuilder">コマンドを設定するメソッド。</param>
        public void Update(Action<SqlCommand> commandBuilder)
        {
            using (var cmd = this.cnn.CreateCommand())
            {
                cmd.Transaction = this.trans;
                commandBuilder(cmd);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>コミットする。
        /// </summary>
        public void Commit()
        {
            this.trans.Commit();
        }

        /// <summary>ロールバックする。
        /// </summary>
        public void RollBack()
        {
            this.trans.Rollback();
        }

        /// <summary>コマンドの内容をイミディエイトウィンドウに表示する。
        /// </summary>
        /// <param name="cmd"></param>
        private void DebugWriteCommand(SqlCommand cmd)
        {
            Debug.WriteLine(new string('-', 12));

            string  typeString;
            string valueString;
            foreach (SqlParameter parameter in cmd.Parameters)
            {
                switch (parameter.SqlDbType)
                {
                    case     SqlDbType.DateTime: typeString = "datetime"; valueString = string.Format("'{0}'", parameter.Value); break;
                    case     SqlDbType.Decimal:  typeString =  "numeric"; valueString = string.Format("{0}",   parameter.Value); break;
                    case     SqlDbType.Int:      typeString =      "int"; valueString = string.Format("{0}",   parameter.Value); break;
                    case     SqlDbType.VarChar:  typeString =  "varchar"; valueString = string.Format("'{0}'", parameter.Value); break;
                    default:                     typeString =  "varchar"; valueString = string.Format("'{0}'", parameter.Value); break;
                }

                Debug.WriteLine("DECLARE {0} AS {1}", parameter.ParameterName, typeString);
                Debug.WriteLine("SET {0} = {1}", parameter.ParameterName, valueString);
            }

            Debug.WriteLine(cmd.CommandText);

            Debug.WriteLine(new string('-', 12));
            Debug.WriteLine(new string('-', 12));
        }

        /// <summary>リソースを解放する。
        /// </summary>
        public void Dispose()
        {
            if (this.trans != null) { this.cnn.Dispose(); }
            if (this.cnn != null) { this.cnn.Dispose(); }
        }
    }
}
