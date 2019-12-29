using FineCore.Attributes;
using System;

namespace FineCore.M {
    public abstract class BaseModel {
        [ColumnName("Id",true)]
        public int Id { get; set; } = 0;

        public bool Actived { get; set; } = true;

        public object this[string propertyName] {
            get {
                var pi = GetType().GetProperty(propertyName);
                if (pi == null) return null;
                return pi.GetValue(pi.GetValue(this));
            }
            set {
                if (this == null) { throw new NullReferenceException("序号：M.00000001，当前对象为null,请先完成初始化才可赋值。"); }
                var pi = GetType().GetProperty(propertyName);
                if (pi != null) {
                    pi.SetValue(this, value);
                } else {
                    throw new NullReferenceException("序号：M.00000002，当前对象未找到该属性，请联系管理员。");
                }
            }
        }

    }
}
