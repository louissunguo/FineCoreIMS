
using FineCore.Attributes;
using System.Threading;

namespace  FineCore.M.Shared {
    [TableName("NameInf")]
    public class NameInf : BaseModel {
        private static string language = Thread.CurrentThread.CurrentCulture.Name;
        public string RefTable { get; set; }
        public int RefModelId { get; set; }
        public string DisplayText { get; set; }
        public string Language { get; set; } = language;

    }
}
