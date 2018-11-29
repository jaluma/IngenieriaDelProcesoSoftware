using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    public class CountAthleteByDniLogic : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly string _dni;

        public int Contador;

        public CountAthleteByDniLogic(ref DBConnection conn, string dni) {
            _conn = conn;
            _dni = dni;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_COUNT_ATHLETE_BY_DNI, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _dni);
                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) Contador = reader.GetInt32(0);
                    }
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
