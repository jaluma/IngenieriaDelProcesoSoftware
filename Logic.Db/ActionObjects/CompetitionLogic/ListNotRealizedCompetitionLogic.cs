using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class ListNotRealizedCompetitionLogic : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly DataTable Table;

        public ListNotRealizedCompetitionLogic(ref DBConnection conn) {
            _conn = conn;
            Table = new DataTable();
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_COMPETITION_FINISH_LIST, _conn.DbConnection)) {
                    var da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
