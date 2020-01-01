using FineCore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FineCore.M.Shared {
    [TableName("FuncInf")]
    public partial class FuncInf : NamedModel {
        /// <summary>
        /// 页面完整路径（用于网站版）
        /// </summary>
        [ColumnName("PageUrl")]
        public string FullPagePath { get; set; }

        /// <summary>
        /// 类命名（用于WinForm版）
        /// </summary>
        [ColumnName("FullClassName")]
        public string FullClassName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [ColumnName("DescriptionText")]
        public string Description { get; set; }

    }
}
