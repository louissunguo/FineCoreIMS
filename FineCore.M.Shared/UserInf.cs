using FineCore.Attributes;
using System.Collections.Generic;

namespace FineCore.M.Shared {
    [TableName("UserInf")]
    public partial class UserInf: NamedModel {
        [ColumnName("UserName",true)]
        [StringValidator(5,50)]
        public string UserName { get; set; }

        [ColumnName("LoginPwd")]
        [StringValidator(8, 20)]
        public string Password { get; set; }

        [StringValidator(8, 64)]
        [ColumnName("EmailUrl", true)]
        public string Email { get; set; }

        [ColumnName("MobileNo", true)]
        [StringValidator(11, 11)]
        public string MobileNo { get; set; }

        [ColumnName("WechatNo", true)]
        [StringValidator(5, 64)]
        public string WechatNo { get; set; }

        public List<FuncInf> Funcs = new List<FuncInf>();

    }
}
