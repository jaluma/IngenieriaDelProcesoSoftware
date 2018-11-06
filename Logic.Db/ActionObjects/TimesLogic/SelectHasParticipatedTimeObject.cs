using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.TimesLogic {

    public class SelectHasParticipatedTimeObject : IActionObject {
        private readonly DBConnection _conn;
        public readonly HasParticipatedDto HasParticipated;
        private readonly CompetitionDto _competition;
        private readonly AthleteDto _athlete;

        public SelectHasParticipatedTimeObject(ref DBConnection conn, CompetitionDto competition, AthleteDto athlete) {
            _conn = conn;
            HasParticipated = new HasParticipatedDto();
            _competition = competition;
            _athlete = athlete;
        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_HAS_PARTICIPATED, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);

                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        
                        HasParticipated.Competition = _competition;
                        HasParticipated.Athlete = _athlete;
                        HasParticipated.InitialTime = reader.GetInt64(2);
                        HasParticipated.FinishTime = reader.IsDBNull(3) ? 0 : reader.GetInt64(3);
                    }
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
