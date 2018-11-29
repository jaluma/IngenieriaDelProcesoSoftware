using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.TimesLogic
{
    public class SelectHasParticipatedTimeObject : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;
        private readonly DBConnection _conn;
        public readonly HasParticipatedDto HasParticipated;

        public SelectHasParticipatedTimeObject(ref DBConnection conn, CompetitionDto competition, AthleteDto athlete) {
            _conn = conn;
            HasParticipated = new HasParticipatedDto();
            _competition = competition;
            _athlete = athlete;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_HAS_PARTICIPATED, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);

                    using (var reader = command.ExecuteReader()) {
                        reader.Read();

                        try {
                            HasParticipated.Competition = _competition;
                            HasParticipated.Athlete = _athlete;
                            HasParticipated.InitialTime = reader.IsDBNull(2) ? 0 : reader.GetInt64(2);
                            HasParticipated.FinishTime = reader.IsDBNull(3) ? 0 : reader.GetInt64(3);
                        }
                        catch (InvalidOperationException) { }
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
