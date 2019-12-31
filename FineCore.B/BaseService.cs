using FineCore.B.Interfaces;
using System;

namespace FineCore.B {
    public abstract class BaseService : IBaseService {
        /// <summary>
        /// 检验登陆
        /// </summary>
        /// <param name="userKeyField">账号类型（映射的数据库字段）</param>
        /// <param name="userKeyValue">账号</param>
        /// <param name="loginPwd">密码</param>
        /// <returns>是否登入</returns>
        public bool CheckLogin(string userKeyField, string userKeyValue, string loginPwd) {
            throw new NotImplementedException();
        }
    }
}
