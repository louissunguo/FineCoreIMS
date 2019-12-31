﻿using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;

namespace FineCore.DB {
    /// <summary>
    /// 数据库设置
    /// </summary>
    public static class DbSettings {

        #region 配置文件

        /// <summary>
        /// 配置文件目录
        /// </summary>
        private static string configDir {
            get {
                try {
                    var dir = Environment.CurrentDirectory + "\\Configs";
                    if (Directory.Exists(dir)) return dir;
                    else return System.Environment.CurrentDirectory;
                } catch (Exception ex) {
                    throw new Exception($"序号：FineCore.DB.DbSettings.00000001{ex.Message}");
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
                    throw new Exception($"序号：FineCore.DB.DbSettings.00000002{ex.Message}");
                }
            }
        }

        /// <summary>
        /// 文本读取器
        /// </summary>
        private static TextReader textReader {
            get {
                try {
                    var content = streamReader.ReadToEnd();
                    return new StringReader(content);
                } catch (Exception ex) {
                    throw new Exception($"序号：FineCore.DB.DbSettings.00000003{ex.Message}");
                }
            }
        }

        /// <summary>
        /// JSON文本读取器
        /// </summary>
        private static JsonReader Reader { get { return new JsonTextReader(textReader); } }

        #endregion

        /// <summary>
        /// Database驱动：DbProvider
        /// </summary>
        public static string DbProvider { get { var value = Reader.Read()? $"{Reader.Value}":string.Empty; return value; } }



    }
}