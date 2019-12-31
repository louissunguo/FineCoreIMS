using FineCore.Attributes;
using System;
using System.Reflection;
using System.Threading;

namespace FineCore.M {
    public abstract class BaseModel {
        #region 可传递到客户端

        [ColumnName("Id", true)]
        public int Id { get; set; } = 0;

        [ColumnName("Actived")]
        public bool Actived { get; set; } = true;

        #endregion

        #region 本地使用


        public string CurrentSystemLanguage { get { return Thread.CurrentThread.CurrentCulture.Name; } }

        public string GetTableName() {
            var type = GetType();
            if (type.IsDefined(typeof(TableNameAttribute), true)) {
                var custAttribute = GetType().GetCustomAttribute<TableNameAttribute>(true);
                return custAttribute.TableName;
            } else {
                return null;
                throw new ArgumentNullException("序号：BaseModel.00000004，您给的实例不是数据表映射对象，所以，没有表名称。");
            }
        }

        public string GetColumnName(string propertyName) {
            var pi = GetType().GetProperty(propertyName);
            if (pi == null) return null;
            if (pi.IsDefined(typeof(ColumnNameAttribute), true)) {
                var fieldAttribute = pi.GetCustomAttribute<ColumnNameAttribute>(true);
                return fieldAttribute.ColumnName;
            } else {
                throw new ArgumentNullException("序号：BaseModel.00000003，该属性不映射数据表字段。");
            }
        }

        public object this[string propertyName] {
            get {
                var pi = GetType().GetProperty(propertyName);
                if (pi == null) return null;
                return pi.GetValue(pi.GetValue(this));
            }
            set {
                if (this == null) { throw new NullReferenceException("序号：BaseModel.00000001，当前对象为null,请先完成初始化才可赋值。"); }
                var pi = GetType().GetProperty(propertyName);
                if (pi != null && pi.CanWrite) {
                    pi.SetValue(this, value);
                } else if (pi != null) {
                    throw new NullReferenceException("序号：BaseModel.00000002_01，当前对象未找到该属性，请联系管理员。");
                } else {
                    throw new MemberAccessException("序号：BaseModel.00000002_2，当前对象的这个属性为只读属性，请联系管理员。");
                }
            }
        }

        #endregion
    }
}
