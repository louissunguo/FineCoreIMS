using System;

namespace  FineCore.Attributes {
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldAttribute : AttributeBase {

        public string FieldName { get; private set; }
        public string Operator { get; private set; }

        private FieldAttribute() { }

        public FieldAttribute(string FieldName, string Operator) {
            this.FieldName = FieldName;
            this.Operator = Operator;
        }

        public override bool Validate(object value) { return true; }

        public override string ToString() {
            return base.ToString();
        }
    }
}
