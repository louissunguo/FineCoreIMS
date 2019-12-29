using FineCore.Attributes;

namespace  FineCore.M.Shared {
    [TableName("UserInf")]
    public class UserInf: NamedModel {
        [ColumnName("UserName",true)]
        [StringValidator(5,50)]
        public string UserName { get; set; }

        [StringValidator(8, 20)]
        public string LoginPwd { get; set; }

        [StringValidator(8, 64)]
        [ColumnName("EmailUrl", true)]
        public string EmailUrl { get; set; }

        [ColumnName("MobileNo", true)]
        [StringValidator(11, 11)]
        public string MobileNo { get; set; }

        [ColumnName("WechatNo", true)]
        [StringValidator(5, 64)]
        public string WechatNo { get; set; }

    }
}
