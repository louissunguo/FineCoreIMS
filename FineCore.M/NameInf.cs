
using FineCore.Attributes;
using System.Threading;

namespace FineCore.M {
    [TableName("NameInf")]
    public class NameInf : BaseModel {

        [ColumnName("RefTable")]
        public string RefTable { get; set; }

        [ColumnName("RefModelId")]
        public int RefModelId { get; set; }


        [ColumnName("DisplayText")]
        public string DisplayText { get; set; }

        [ColumnName("LanguageName")]
        public string Language { get; set; } = CurrentSystemLanguage;

    }
}
