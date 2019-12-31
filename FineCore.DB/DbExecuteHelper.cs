using System;
using System.Collections.Generic;
using System.Data;

namespace FineCore.DB {
    public static class DbExecuteHelper {

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

        #endregion

        #region 写入（增、删、改）
        //1、
        #region 不用事务方式，多用于单语句（单命令）操作

        #endregion

        ///2、
        #region 使用事务方式，多用于多语句（多命令）操作，需确保数据一致性

        #endregion

        #endregion
    }
}
