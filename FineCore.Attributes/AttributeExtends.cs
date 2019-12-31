using System;
using System.Linq;
using System.Reflection;

namespace FineCore.Attributes {
    public static class AttributeExtends {
        /// <summary>
        /// 特性校验
        /// </summary>
        /// <typeparam name="T">需要通过特性校验数据的对象的类型</typeparam>
        /// <typeparam name="TAttribute">要校验的特性</typeparam>
        /// <param name="model">要校验数据的对象</param>
        /// <returns>是否通过校验</returns>
        public static bool Validate<T, TAttribute>(T model) where TAttribute : AttributeBase {

            if (model == null) return false;
            var type = model.GetType();
            var piArr = type.GetProperties();
            var isValid = true;
            foreach (var pi in piArr) {
                var val = pi.GetValue(model);
                if (pi.IsDefined(typeof(TAttribute), true)) {
                    var att = pi.GetCustomAttribute<TAttribute>(true);
                    if (att != null) isValid &= att.Validate(val);
                }
            }

            return isValid;
        }

    }
}
