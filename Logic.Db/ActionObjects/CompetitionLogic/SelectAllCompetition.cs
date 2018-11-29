using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class SelectAllCompetition : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly DataTable Table;

        public SelectAllCompetition(ref DBConnection conn) {
            _conn = conn;
            Table = new DataTable();
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_ALL_COMPETITIONS, _conn.DbConnection)) {
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
