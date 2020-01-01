using FineCore.Attributes;

namespace FineCore.M.Shared {
    [TableName("DeptInf")]
    public partial class DeptInf : NamedModel {
        /// <summary>
        /// 部门经理
        /// </summary>
        [ColumnName("DeptManagerId",false,true)]
        public int DeptManagerId { get; set; }

        /// <summary>
        /// 部门秘书
        /// </summary>
        [ColumnName("DeptSecretaryId",false,true)]
        public int DeptSecretaryId { get; set; }

        /// <summary>
        /// 分机号
        /// </summary>
        [ColumnName("ExtNo")]
        [StringValidator(2,4)]
        public string ExtNo { get; set; }

    }
}
