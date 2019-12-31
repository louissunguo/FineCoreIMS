﻿using FineCore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.M.Shared {
    [TableName("RoleInf")]
    public class RoleInf :NamedModel {

        /// <summary>
        /// 描述
        /// </summary>
        [ColumnName("DescriptionText")]
        public string Description { get; set; }

    }
}