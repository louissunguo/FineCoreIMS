using FineCore.Attributes;
using FineCore.M;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace FineCore.DB {
    public static partial class DbExecuteHelper {

        #region 读取

        public static DataSet GetDataSet(IDbCommand cmd, IEnumerable<IDataParameter> paras = null) {
            var ds = new DataSet();
            var adapter = DbSettings.GetDataAdapter(cmd, paras);
            adapter.Fill(ds);
            adapter = null;
            return ds;
        }

        public static DataSet GetDataSet(IDbDataAdapter adapter) {
            var ds = new DataSet();
            adapter.Fill(ds);
            adapter = null;
            return ds;
        }

        public static DataSet GetDataSet(string cmdText, string connString, IEnumerable<IDataParameter> paras, CommandType cmdType = CommandType.Text) {
            var da = DbSettings.GetDataAdapter(cmdText, connString, paras, cmdType);
            var ds = new DataSet();
            da.Fill(ds);
            da = null;
            return ds;
        }

        /******************************************************************************************************************************************************/

        public static List<T> GetModels<T>(string cmdText, string connString, IEnumerable<IDataParameter> paras, CommandType cmdType = CommandType.Text) where T : BaseModel, new() {
            var cmd = DbSettings.GetCommand(cmdText, paras, cmdType);
            var con = DbSettings.GetConnection(false, connString);
            if (cmd != null && con != null) cmd.Connection = con;
            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
            var reader = cmd.Connection != null && cmd.Connection.State == ConnectionState.Open ? cmd.ExecuteReader() : null;
            if (reader == null) return new List<T>();
            List<T> li = new List<T>();
            var piArray = typeof(T).GetType().GetProperties().Where(p => p.CanWrite && p.IsDefined(typeof(ColumnNameAttribute), true));
            while (reader.Read()) {
                T m = default(T);
                foreach (var pi in piArray) {
                    var colName = pi.GetCustomAttribute<ColumnNameAttribute>().ColumnName;
                    m[pi.Name] = reader[colName];
                }
                li.Add(m);
            }
            return li;
        }

        public static List<T> GetModels<T>(IDbDataAdapter adapter) where T : BaseModel, new() {
            var ds = new DataSet();
            adapter.Fill(ds);
            adapter = null;
            return GetModelsFromDataSet<T>(ds);
        }

        private static List<T> GetModelsFromDataSet<T>(DataSet ds) where T : BaseModel, new() {
            if (ds == null || ds.Tables == null || ds.Tables.Count != 1 || ds.Tables[0].Rows == null || ds.Tables[0].Rows.Count <= 0) return new List<T>();

            var li = new List<T>();
            var piArray = typeof(T).GetType().GetProperties().Where(p => p.CanWrite && p.IsDefined(typeof(ColumnNameAttribute), true));
            foreach (DataRow dr in ds.Tables[0].Rows) {
                T m = default(T);
                foreach (var pi in piArray) {
                    var colName = pi.GetCustomAttribute<ColumnNameAttribute>().ColumnName;
                    m[pi.Name] = dr[colName];
                }
                li.Add(m);
            }
            return li;
        }

        public static List<T> GetModels<T>(IDbCommand cmd, IEnumerable<IDataParameter> paras = null) where T : BaseModel, new() {
            var ds = new DataSet();
            var adapter = DbSettings.GetDataAdapter(cmd, paras);
            adapter.Fill(ds);
            adapter = null;
            return GetModelsFromDataSet<T>(ds);
        }

        #endregion

        #region 写入（增、删、改）
        //1、
        #region 不用事务方式，多用于单语句（单命令）操作

        public static int ExecuteSQL(string cmdText, string connString, IEnumerable<IDataParameter> paras, CommandType cmdType = CommandType.Text) {
            var cmd = DbSettings.GetCommand(cmdText, paras, cmdType);
            var con = DbSettings.GetConnection(true, connString);
            if (cmd != null && cmd.Connection == null) cmd.Connection = con;
            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
            var rows = cmd.ExecuteNonQuery();
            return rows;
        }

        public static int ExecuteSQL(IDbCommand cmd, IEnumerable<IDataParameter> paras = null) {
            if (cmd != null && paras != null) {
                foreach (var p in paras) {
                    if (!cmd.Parameters.Contains(p)) cmd.Parameters.Add(p);
                }
            }
            var rows = cmd.ExecuteNonQuery();
            return rows;
        }

        #endregion

        ///2、
        #region 使用事务方式，多用于多语句（多命令）操作，需确保数据一致性

        public static int ExecuteSQL(List<DbCommandSet> cmdSets, string connString = null) {
            var conn = DbSettings.GetConnection(true, connString);
            var tran = conn.BeginTransaction();
            var rows = 0;
            try {
                foreach (var cmdSet in cmdSets) {
                    cmdSet.Command.Connection = conn;
                    cmdSet.Command.Transaction = tran;
                    rows += cmdSet.Command.ExecuteNonQuery();
                }
                tran.Commit();
            } catch (Exception ex) {
                tran.Rollback();
                rows = 0;
                throw ex;
            } finally {
                DisposeDbSource(conn, tran);
            }
            return rows;
        }
        /// <summary>
        /// 快速释放数据库访问资源
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="tran">事务</param>
        private static void DisposeDbSource(IDbConnection conn, IDbTransaction tran) {
            if (tran != null) tran.Dispose();
            if (conn != null && conn.State != ConnectionState.Closed) conn.Close();
        }

        public static int ExecuteSQL(IEnumerable<IDbCommand> cmds, string connString = null) {
            var conn = DbSettings.GetConnection(true, connString);
            var tran = conn.BeginTransaction();
            var rows = 0;
            try {
                foreach (var cmd in cmds) {
                    cmd.Connection = conn;
                    cmd.Transaction = tran;
                    rows += cmd.ExecuteNonQuery();
                }
                tran.Commit();
            } catch (Exception ex) {
                tran.Rollback();
                rows = 0;
                throw ex;
            } finally {
                DisposeDbSource(conn, tran);
            }
            return rows;
        }

        #endregion

        #endregion
    }
}
