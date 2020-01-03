using FineCore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.M {
    
    public abstract class NamedModel : BaseModel {

        [ColumnName("Code")]
        [StringValidator(1, 16)]
        public string Code { get; set; }

        public List<NameInf> Names = new List<NameInf>();

    }
}
