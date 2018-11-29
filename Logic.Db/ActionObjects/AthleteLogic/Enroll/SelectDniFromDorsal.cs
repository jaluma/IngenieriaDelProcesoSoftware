using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class SelectDniFromDorsal : IActionObject
    {
        private readonly long _competitionId;
        private readonly DBConnection _conn;
        private readonly int _dorsal;
        public string Dni;

        public SelectDniFromDorsal(ref DBConnection conn, long competitionId, int dorsal) {
            _conn = conn;
            _competitionId = competitionId;
            _dorsal = dorsal;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_DNI_FROM_DORSAL, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DORSAL", _dorsal);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competitionId);

                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) Dni = reader.GetString(0);
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
