using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    public class DeleteAthleteLogic : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly DBConnection _conn;

        public DeleteAthleteLogic(ref DBConnection conn, AthleteDto athleteP) {
            _conn = conn;
            _athlete = athleteP;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_DELETE_ATHLETE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
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
