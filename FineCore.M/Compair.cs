
using FineCore.Attributes;
using System.Threading;

namespace FineCore.M {
    public enum Compair {

        [Field("开始于", "LIKE")]
        BeginWith = 1,

        [Field("结束于", "LIKE")]
        EndWith = 2,

        [Field("包含", "LIKE")]
        Contains = 3,

        [Field("大于", ">")]
        MoreThan = 4,

        [Field("等于", "=")]
        Equals = 5,

        [Field("大于等于", ">=")]
        MoreOrEqualsThan = 6,

        [Field("小于等于", "<=")]
        LessOrEqualsThan = 7,

        [Field("小于", "<")]
        LessThan = 8,

        [Field("介于", "Between")]
        Between = 9,

        [Field("不等于", "!=")] //只支持SQL Server2008之后的版本，或MySQL/Oracle/SQLite等
        NotEquals = 10

    }
}
