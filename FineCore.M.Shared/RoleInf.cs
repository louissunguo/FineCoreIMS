using FineCore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.M.Shared {
    [TableName("RoleInf")]
    public partial class RoleInf :NamedModel {

        /// <summary>
        /// 描述
        /// </summary>
        [ColumnName("DescriptionText")]
        [StringValidator(0,256)]
        public string Description { get; set; }

    }
}
