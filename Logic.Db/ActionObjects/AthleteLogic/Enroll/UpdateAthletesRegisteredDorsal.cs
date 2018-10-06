using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {

    public class UpdateAthletesRegisteredDorsal : IActionObject {

        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        public UpdateAthletesRegisteredDorsal(ref DBConnection conn, CompetitionDto competition) {
            _conn = conn;
            _competition = competition;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETES_STATUS, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    command.Parameters.AddWithValue("@STATUS", TypesStatus.Registered.ToString().ToUpper());
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        int dorsal = 21;
                        while (reader.Read()) {
                            using (SQLiteCommand command2 = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_UPDATE_DORSAL, _conn.DbConnection)) {
                                command2.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                                command2.Parameters.AddWithValue("@DNI", reader.GetString(0));
                                command2.Parameters.AddWithValue("@DORSAL", dorsal++);
                                command2.ExecuteNonQuery();
                            }
                        }
                    }
                }
                
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
