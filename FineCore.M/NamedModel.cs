using FineCore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.M {
    
    public abstract class NamedModel : BaseModel {

        [ColumnName("Code")]
        public string Code { get; set; }

        public List<Compair> Names = new List<Compair>();

    }
}
