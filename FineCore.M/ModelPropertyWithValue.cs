using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.M {


    public class ModelPropertyWithValue {
        private bool ForWrite = true;
        public string PropertyName { get; private set; }

        public string Operator { get; private set; } = "=";

        public object PropertyValue { get; private set; }

        private ModelPropertyWithValue() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_PropertyName">属性名</param>
        /// <param name="_PropertyValue">属性值</param>
        /// <param name="_Operator">比较操作符号</param>
        /// <param name="_ForWrite">是否是写入操作，若是则比较操作符固定为“=”</param>
        public ModelPropertyWithValue(string _PropertyName, object _PropertyValue, Type _PropertyValueType, string _Operator = "=", bool _ForWrite = true) {

            PropertyName = _PropertyName;
            PropertyValue = _PropertyValue ?? DBNull.Value;
            ForWrite = _ForWrite;



            if (ForWrite) {

                Operator = " = ";

            } else {
                if (PropertyValue is DBNull && !Operator.ToUpper().Contains(" IS ")) {
                    Operator = " IS ";
                }
            }

            if (!_PropertyValueType.IsValueType && !(PropertyValue is DBNull)) {
                PropertyValue = $"'{PropertyValue}'";
            }


        }

        public override string ToString() {
            return $"[{PropertyName}] {Operator} {PropertyValue}";
        }
    }
}
