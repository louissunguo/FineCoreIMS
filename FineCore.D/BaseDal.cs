using FineCore.Attributes;
using FineCore.DB;
using FineCore.M;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FineCore.D {
    public static class BaseDal {

        public static int Save<T>(T model) where T : BaseModel, new() {
            var piArray = model.GetType().GetProperties().Where(p => p.IsDefined(typeof(ColumnNameAttribute), true) && p.CanWrite);
            var ColumnNames = piArray.Select(p => model.GetColumnName(p.Name));
            var Parameters = piArray.Select(
                (p) => DbSettings.GetParameter(
                    $"@{model.GetColumnName(p.Name)}",
                    p.GetValue(model),
                    p.Name.ToUpper().EndsWith("ID") && p.PropertyType.IsValueType && p.PropertyType.IsEnum
                )
            );
            var s = model.Id > 0 ? $"UPDATE [{model.GetTableName()}] SET { string.Join(',', ColumnNames.Select(col => $"[{col}]=@{col}")) } where Id=@Id"
                                : $"create table #t(Id int) INSERT INTO [{model.GetTableName()}]({string.Join(',', ColumnNames.Select(col => $"[{col}]"))}) output inserted.Id into #t VALUES({string.Join(',', ColumnNames.Select(col => $"@{col}"))}) select @Id=Id from #t";
            var rows = DbExecuteHelper.ExecuteSQL(s, DbSettings.WriteConnString, Parameters, CommandType.Text);

            return rows;
        }

        public static int Delete(int Id, string tableName) {
            var s = $" UPDATE [{tableName}] set Actived=0 where id=" + Id;
            var rows = DbExecuteHelper.ExecuteSQL(s, null, null, CommandType.Text);
            return rows;
        }

        public static int Delete<T>(int Id) where T : BaseModel, new() {
            var tableName = default(T).GetTableName();
            var s = $" UPDATE [{tableName}] set Actived=0 where id=" + Id;
            var rows = DbExecuteHelper.ExecuteSQL(s, null, null, CommandType.Text);
            return rows;
        }

        public static T GetModel<T>(int Id) where T : BaseModel, new() {
            var tableName = default(T).GetTableName();
            var s = $"select * from [{tableName}] where Id=" + Id;
            var li = DbExecuteHelper.GetModels<T>(s, null, null, CommandType.Text);
            if (li != null && li.Count == 1) return li[0];
            else return null;
        }

        public static List<T> GetModels<T>(List<ModelPropertyWithValue> paras) where T : BaseModel, new() {

            var tableName = default(T).GetTableName();
            var s = $"select * from [{tableName}] where ";

            if (paras != null && paras.Count > 0) {
                foreach (var para in paras) {
                    s += para.ToString() + " AND ";
                }
                s = s.Trim().ToLower().Replace(" and ", string.Empty);
            } else {
                s = s.Trim().ToLower().Replace(" where ", string.Empty);
            }

            var li = DbExecuteHelper.GetModels<T>(s, null, null, CommandType.Text);
            return li;
        }

        public static List<T> GetModelsWaitApproval<T>(int currentApprover) where T : BaseModel, new() {
            var tableName = default(T).GetTableName();
            var s = $"select * from [{tableName}] m " +
                $" inner join join ApprovalInf a on a.[TabName]='{tableName}' and ApprUserId={currentApprover}" +
                $" where m.actived=1 a.and actived=1";
            var li = DbExecuteHelper.GetModels<T>(s, null, null, CommandType.Text);
            return li;
        }

    }
}
