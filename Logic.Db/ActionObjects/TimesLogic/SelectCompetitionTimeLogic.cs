using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.TimesLogic
{
    public class SelectCompetitionTimeLogic : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly DataTable Table;

        public SelectCompetitionTimeLogic(ref DBConnection conn) {
            _conn = conn;
            Table = new DataTable();
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_COMPETITION_STATUS, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@STATUS", "FINISH");

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
