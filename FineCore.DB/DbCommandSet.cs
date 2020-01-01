using System.Collections.Generic;
using System.Data;

namespace FineCore.DB {
    public partial class DbCommandSet {

        private IDbCommand _Command=null;

        public IDbCommand Command { get { return _Command; } private set { _Command = DbSettings.GetCommand(CommandText, DataParameters, CommandType); } }

        public string CommandText { private get; set; }

        public CommandType CommandType { private get; set; } = CommandType.Text;

        public IEnumerable<IDataParameter> DataParameters { private get; set; }

    }
}
