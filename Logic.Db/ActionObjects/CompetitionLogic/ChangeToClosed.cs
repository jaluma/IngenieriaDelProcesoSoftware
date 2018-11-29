using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class ChangeToClosed : IActionObject
    {
        private readonly long _competition;


        private readonly DBConnection _conn;


        public ChangeToClosed(ref DBConnection conn, long id) {
            _competition = id;
            _conn = conn;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_CHANGE_CLOSED_STATUS, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition);
                    command.Parameters.AddWithValue("@STATUS", "CLOSED");


                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
