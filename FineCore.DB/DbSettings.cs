using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FineCore.DB {
    /// <summary>
    /// 数据库设置
    /// </summary>
    public static partial class DbSettings {
        private static int seed = 0;
        private static string[] connStringArrayForRead = GetReadConnStringArry();

        public static Dictionary<int, IDbConnection> DbReadConnections {
            get {
                var dic = new Dictionary<int, IDbConnection>();
                var conArr = connStringArrayForRead.Select((connStr, i) => new KeyValuePair<int, IDbConnection>(i++, GetConnection(false, connStr)));
                foreach (var c in conArr) { if (!dic.ContainsKey(c.Key)) dic.Add(c.Key, c.Value); }
                return dic;
            }
        }

        #region 配置文件

        /// <summary>
        /// 配置文件目录
        /// </summary>
        private static string configDir {
            get {
                try {
                    var dir = Environment.CurrentDirectory + "\\Configs";
                    if (Directory.Exists(dir))
                        return dir;
                    else
                        return System.Environment.CurrentDirectory;
                } catch (Exception ex) {
                    throw new Exception($"序号：FineCore.DB.DbSettings.00000001；{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 加载配置文件，文件名默认为“db.json”，并且不可变更，
        /// 在布署项目时，一定要将此配置文件放在系统的配置文件目录
        /// (若未有Configs目录，则放置在当前系统运行的目录)
        /// 该配置文件中指定数据访问驱动
        /// </summary>
        private static StreamReader streamReader {
            get {
                try {
                    return new StreamReader(configDir + "\\db.json");
                } catch (Exception ex) {
                    throw new Exception($"序号：FineCore.DB.DbSettings.00000002；{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 文本读取器
        /// </summary>
        private static JObject dbSettings {
            get {
                try {
                    var content = streamReader.ReadToEnd();
                    var jsonObj = JsonConvert.DeserializeObject<JObject>(content);
                    return jsonObj;
                } catch (Exception ex) {
                    throw new Exception($"序号：FineCore.DB.DbSettings.00000003；{ex.Message}");
                }
            }
        }

        #endregion

        #region 取系统配置信息

        /// <summary>
        /// 取Database驱动：GetDbProvider
        /// </summary>
        public static string GetDbProvider(string settingKey = "dbProvider") {
            return dbSettings.Value<string>(settingKey);
        }

        public static string GetDbAssembly(string settingKey = "dbAssembly") {
            return dbSettings.Value<string>(settingKey);
        }

        public static string GetDbObjectHeadString(string settingKey = "dbObjectHeadString") {
            return dbSettings.Value<string>(settingKey);
        }


        /// <summary>
        /// 取Database连接字符串（写数据）
        /// </summary>
        public static string GetWriteConnString(string settingKey = "dbConnWrite") {
            return dbSettings.Value<string>(settingKey);
        }

        /// <summary>
        /// 取Database连接字符串（只读数据）
        /// </summary>
        private static string[] GetReadConnStringArry(string settingKey = "dbConnRead") {
            return dbSettings.Values<string>(settingKey).ToArray();
        }

        /// <summary>
        /// 取得分配的可读取数据库连接（此处为方法）
        /// </summary>
        public static string GetReadConnString() {
            string settingKey = "dbConnRead";
            if (connStringArrayForRead == null) connStringArrayForRead = GetReadConnStringArry(settingKey);

            var random = new Random(++seed / connStringArrayForRead.Length);
            var index = random.Next(0, connStringArrayForRead.Length);

            return connStringArrayForRead[index];
        }

        /// <summary>
        /// 可读取的数据库连接（此处为属性）
        /// </summary>
        public static string ReadConnString { get { return GetReadConnString(); } }

        public static string WriteConnString { get { return GetWriteConnString(); } }

        #endregion


        #region 取得Connection、Command、DataAdapter

        public static IDbConnection GetConnection(bool canWrite = false, string connString = null) {
            connString = string.IsNullOrEmpty(connString) || string.IsNullOrWhiteSpace(connString) ? (canWrite ? WriteConnString : ReadConnString) : connString;
            string dbObjType = "Connection";
            try {
                var assembly = Assembly.LoadFrom(GetDbAssembly());
                var headStr = GetDbObjectHeadString();
                var conn = (IDbConnection)assembly.GetType(headStr + dbObjType);
                if (conn != null) conn.ConnectionString = connString;
                return conn;
            } catch (Exception ex) {
                return null;
                throw new Exception($"序号：FineCore.DB.DbSettings.00000004_1；{ex.Message}");
            }
        }

        /// <summary>
        /// 生成查询命令
        /// </summary>
        /// <param name="cmdText">查询语句</param>
        /// <param name="paras">查询参数</param>
        /// <param name="cmdType">查询语句类型</param>
        /// <returns></returns>
        public static IDbCommand GetCommand(string cmdText = null, IEnumerable<IDataParameter> paras = null, CommandType cmdType = CommandType.Text) {
            string dbObjType = "Command";
            try {
                var assembly = Assembly.LoadFrom(GetDbAssembly());
                var headStr = GetDbObjectHeadString();
                var cmd = (IDbCommand)assembly.GetType(headStr + dbObjType);
                if (cmd != null) {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;
                    if (paras != null) {
                        foreach (var para in paras) {
                            if (para != null && !cmd.Parameters.Contains(para)) cmd.Parameters.Add(para);
                        }
                    }
                }
                return cmd;
            } catch (Exception ex) {
                return null;
                throw new Exception($"序号：FineCore.DB.DbSettings.00000004_2；{ex.Message}");
            }
        }

        /// <summary>
        /// 数据适配器
        /// </summary>
        /// <param name="cmdText">查询命令</param>
        /// <param name="connString">连接字符串</param>
        /// <param name="paras">查询参数IDataParameter</param>
        /// <param name="cmdType">查询命令类型CommandType</param>
        /// <returns>返回数据适配器</returns>
        public static IDbDataAdapter GetDataAdapter(string cmdText, string connString, IEnumerable<IDataParameter> paras, CommandType cmdType = CommandType.Text) {
            string dbObjType = "DataAdapter";

            try {
                var cmd = GetCommand(cmdText, paras, cmdType);
                var assembly = Assembly.LoadFrom(GetDbAssembly());
                var headStr = GetDbObjectHeadString();
                var adapter = (IDbDataAdapter)assembly.GetType(headStr + dbObjType);
                if (adapter != null) { adapter.SelectCommand = cmd; adapter.SelectCommand.Connection = GetConnection(false, connString); }
                return adapter;
            } catch (Exception ex) {
                return null;
                throw new Exception($"序号：FineCore.DB.DbSettings.00000004_3_0；{ex.Message}");
            }
        }

        /// <summary>
        /// 取数据适配器
        /// </summary>
        /// <param name="cmd">查询命令</param>
        /// <param name="paras">查询参数</param>
        /// <returns>返回适配器</returns>
        public static IDbDataAdapter GetDataAdapter(IDbCommand cmd, IEnumerable<IDataParameter> paras = null) {
            string dbObjType = "DataAdapter";
            try {
                var assembly = Assembly.LoadFrom(GetDbAssembly());
                var headStr = GetDbObjectHeadString();
                var adapter = (IDbDataAdapter)assembly.GetType(headStr + dbObjType);
                if (adapter != null) {
                    if (paras != null) {
                        foreach (var para in paras) {
                            if (para != null) cmd.Parameters.Add(para);
                        }
                    }
                    adapter.SelectCommand = cmd;
                }
                return adapter;
            } catch (Exception ex) {
                return null;
                throw new Exception($"序号：FineCore.DB.DbSettings.00000004_3_1；{ex.Message}");
            }
        }

        /// <summary>
        /// 构造Parameter
        /// </summary>
        /// <param name="paraName"></param>
        /// <param name="paraValue"></param>
        /// <param name="isId">是否是可由系统自动产生的Id,默认为否</param>
        /// <returns></returns>
        public static IDataParameter GetParameter(string paraName, object paraValue, bool isId = false) {
            string dbObjType = "Parameter";
            paraValue = paraValue ?? DBNull.Value;
            try {
                var assembly = Assembly.LoadFrom(GetDbAssembly());
                var headStr = GetDbObjectHeadString();
                var para = (IDataParameter)assembly.GetType(headStr + dbObjType);
                if (para != null) { para.ParameterName = paraName; para.Value = isId && int.Parse($"{paraValue}") <= 0 ? DBNull.Value : paraValue; }
                return para;
            } catch (Exception ex) {
                return null;
                throw new Exception($"序号：FineCore.DB.DbSettings.00000004_5(构造Parameter失败)；{ex.Message}");
            }
        }


        #endregion
    }
}
