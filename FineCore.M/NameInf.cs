
using FineCore.Attributes;
using System.Threading;

namespace FineCore.M {
    [TableName("NameInf")]
    public partial class NameInf : BaseModel {

        [ColumnName("RefTable")]
        [StringValidator(1,128)]
        public string RefTable { get; set; }

        [ColumnName("RefModelId")]
        public int RefModelId { get; set; }


        [ColumnName("DisplayText")]
        [StringValidator(1, 128)]
        public string DisplayText { get; set; }

        [ColumnName("LanguageName")]
        [StringValidator(1, 6)]
        public string Language { get; set; } = CurrentSystemLanguage;

    }
}
