using FineCore.Attributes;
using FineCore.DB;
using FineCore.M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FineCore.D {
    public class BaseDal {

        public static int Save<T>(T model) where T : BaseModel {
            var piArray = model.GetType().GetProperties().Where(p => p.IsDefined(typeof(ColumnNameAttribute), true) && p.CanWrite);
            var ColumnNames = piArray.Select(p => model.GetColumnName(p.Name));
            var Parameters = piArray.Select(
                (p) => DbSettings.GetParameter(
                    $"@{model.GetColumnName(p.Name)}",
                    p.GetValue(model),
                    p.Name.ToUpper().EndsWith("ID") && p.PropertyType.IsValueType && p.PropertyType.IsEnum
                )
            );
            var s = model.Id > 0 ? $"UPDATE [{model.GetTableName()}] SET { string.Join(',',ColumnNames.Select(col=>$"[{col}]=@{col}")) }"
                                : $"INSERT INTO [{model.GetTableName()}]({string.Join(',', ColumnNames.Select(col=>$"[{col}]"))}) VALUES({string.Join(',', ColumnNames.Select(col => $"@{col}"))})";
            DbExecuteHelper.ExecuteSQL(s, DbSettings.WriteConnString, Parameters, System.Data.CommandType.Text);

            return 0;
        }

    }
}
