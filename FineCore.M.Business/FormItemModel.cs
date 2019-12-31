using FineCore.Attributes;

namespace FineCore.M.Business {
    public abstract class FormItemModel:BaseModel {


        /// <summary>
        /// 业务单号
        /// </summary>
        [ColumnName("FormNo", true)]
        [StringValidator(14, 16)]
        public string FormNo { get; set; }

        [ColumnName("SortId")]
        public int SortId { get; set; }

        [ColumnName("Remark")]
        [StringValidator(0,512)]
        public string Remark { get; set; }
    }
}
