using System;

namespace FineCore.Attributes {
    [AttributeUsage(AttributeTargets.Field)]
    public partial class FieldAttribute : AttributeBase {
        public string FieldName { get; private set; }
        public string Operator { get; private set; }
        private FieldAttribute() { }
        public FieldAttribute(string FieldName, string Operator) { this.FieldName = FieldName; this.Operator = Operator; }
    }
}
