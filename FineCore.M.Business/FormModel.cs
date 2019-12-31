using FineCore.Attributes;
using System;

namespace FineCore.M.Business {
    public abstract class FormModel :BaseModel{

        /// <summary>
        /// 业务单号
        /// </summary>
        [ColumnName("FormNo",true)]
        [StringValidator(14,16)]
        public string FormNo { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ColumnName("UserName",false,true)]
        public string UserName { get; set; }
        
        /// <summary>
        /// 建单日期
        /// </summary>
        [ColumnName("CreateDate")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 提交日期
        /// </summary>
        [ColumnName("SubmitDate")]
        public DateTime? SubmitDate { get; set; }

        /// <summary>
        /// 完成审批日期
        /// </summary>
        [ColumnName("AuditingDate")]
        public DateTime? AuditingDate { get; set; }

        
    }
}
