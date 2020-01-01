using System;

namespace FineCore.Attributes {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public partial class ColumnNameAttribute : AttributeBase {

        private readonly string _ColumnName = string.Empty;
        private readonly bool _IsPrimaryKey = false;
        private readonly bool _IsForeignKey = false;

        private ColumnNameAttribute() { }

        public ColumnNameAttribute(string ColumnName, bool IsPrimaryKey = false, bool IsForeignKey = false) {
            _ColumnName = ColumnName;
            _IsPrimaryKey = IsPrimaryKey;
            _IsForeignKey = IsForeignKey;
        }

        public string ColumnName { get { return _ColumnName; } }
        public bool IsPrimaryKey { get { return _IsPrimaryKey; } }
        public bool IsForeignKey { get { return _IsForeignKey; } }
    }
}
