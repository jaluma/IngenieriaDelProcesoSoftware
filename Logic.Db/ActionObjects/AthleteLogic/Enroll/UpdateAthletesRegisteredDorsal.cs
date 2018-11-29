using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class UpdateAthletesRegisteredDorsal : IActionObject
    {
        private readonly CompetitionDto _competition;

        private readonly DBConnection _conn;

        public UpdateAthletesRegisteredDorsal(ref DBConnection conn, CompetitionDto competition) {
            _conn = conn;
            _competition = competition;
        }

        public void Execute() {
            try {
                var dorsal = 20;
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_MAX_DORSAL, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        reader.Read();
                        try {
                            dorsal = reader.GetInt32(0);
                        }
                        catch (InvalidCastException) { }
                    }
                }

                using (var command = new SQLiteCommand(Resources.SQL_SELECT_ATHLETE_INSCRIPTION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read())
                            using (var command2 = new SQLiteCommand(Resources.SQL_UPDATE_DORSAL, _conn.DbConnection)) {
                                command2.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                                command2.Parameters.AddWithValue("@DNI", reader.GetString(0));
                                command2.Parameters.AddWithValue("@DORSAL", ++dorsal);
                                command2.ExecuteNonQuery();
                            }
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
