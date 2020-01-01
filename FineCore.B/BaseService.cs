using FineCore.B.Interfaces;
using FineCore.M.Shared;
using System;
using System.Runtime.Serialization;

namespace FineCore.B {
    public abstract class BaseService : IBaseService {

        [DataMember]
        private UserInf _UserInf;
        
        public UserInf CurrentUser { get { return _UserInf; } }


        /// <summary>
        /// 检验登陆
        /// </summary>
        /// <param name="userKeyField">账号类型（映射的数据库字段）</param>
        /// <param name="userKeyValue">账号</param>
        /// <param name="loginPwd">密码</param>
        /// <returns>是否登入</returns>
        public bool CheckLogin(string userKeyField, string userKeyValue, string loginPwd) {
            _UserInf = new UserInf();



            return _UserInf != null && _UserInf.Id > 0;
        }

    }
}
