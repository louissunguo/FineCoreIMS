using System;

namespace FineCore.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : AttributeBase {

        public string TableName { get; private set; } = string.Empty;

        private TableNameAttribute() { }

        public TableNameAttribute(string TableName) { this.TableName = TableName; }

    }
}
