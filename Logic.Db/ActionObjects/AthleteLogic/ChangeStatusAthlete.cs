using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    internal class ChangeStatusAthlete
    {
        private readonly AthleteDto _athlete;

        private readonly DBConnection _conn;
        private readonly long _id;

        public ChangeStatusAthlete(ref DBConnection conn, AthleteDto athleteP, long id) {
            _athlete = athleteP;
            _conn = conn;
            _id = id;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_CHANGE_ATHLETE_STATUS, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _id);
                    command.Parameters.AddWithValue("@STATUS", "OUTSTANDING");

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
