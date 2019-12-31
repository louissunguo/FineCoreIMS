using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.Attributes {
    public abstract class AttributeBase:Attribute {

        public virtual bool Validate(object value) { return true; }

    }
}
