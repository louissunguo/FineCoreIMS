using System;

namespace FineCore.Attributes {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ColumnNameAttribute : AttributeBase {

        private string _ColumnName = string.Empty;
        private bool _IsPrimaryKey = false,
                     _IsForeignKey = false;

        private ColumnNameAttribute() { }

        public ColumnNameAttribute(string ColumnName, bool IsPrimaryKey = false, bool IsForeignKey = false) {
            _ColumnName = ColumnName;
            _IsPrimaryKey = IsPrimaryKey;
            _IsForeignKey = IsForeignKey;
        }

        public override bool Validate(object value) { return true; }
    }
}
