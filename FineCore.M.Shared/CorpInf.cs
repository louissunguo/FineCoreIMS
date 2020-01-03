using FineCore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.M.Shared {
    [TableName("CorpInf")]
    public partial class CorpInf : NamedModel {
        /// <summary>
        /// CEO
        /// </summary>

        [ColumnName("CEOId", false, true)]
        public int ChiefExecuteOfficerId { get; set; }

        /// <summary>
        /// 总经理
        /// </summary>
        [ColumnName("GMId", false, true)]
        public int GeneralManagerId { get; set; }

        /// <summary>
        /// CFO
        /// </summary>
        [ColumnName("CFOId", false, true)]
        public int ChiefFinancialOfficerId { get; set; }

        /// <summary>
        /// 出纳
        /// </summary>
        [ColumnName("CashierId", false, true)]
        public int CashierId { get; set; }


        /// <summary>
        /// 电话，固定电话号码
        /// </summary>
        [ColumnName("PhoneNo")]
        [StringValidator(8,11)]
        public string PhoneNo { get; set; }

        /// <summary>
        /// 传真号码
        /// </summary>
        [ColumnName("FaxNo")]
        [StringValidator(8, 11)]
        public string FaxNo { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>
        [ColumnName("PostAddress")]
        [StringValidator(0, 256)]
        public string PostAddress { get; set; }
    }
}
