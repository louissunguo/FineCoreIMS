using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FineCore.Attributes {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StringValidatorAttribute : AttributeBase {


        public int MinSize { get; private set; }
        public int MaxSize { get; private set; }

        public string Expression { get; private set; }

        private StringValidatorAttribute() { }

        public StringValidatorAttribute(int MinSize = 0, int MaxSize = 20, string Expression = null) {
            this.MinSize = MinSize;
            this.MaxSize = MaxSize;
            this.Expression = Expression;
        }

        public override bool Validate(object value) {
            value = value ?? string.Empty;
            var valid = $"{value}".Length >= MinSize && $"{value}".Length <= MaxSize;
            if (string.IsNullOrEmpty(Expression)) return valid;
            var regex = new Regex(Expression);
            var match = regex.Match($"{value}");
            return match.Success;
        }
    }
}
