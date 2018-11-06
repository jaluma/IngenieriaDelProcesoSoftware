using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.TimesLogic {
    public class SelectPartialTimes : IActionObject {
        private readonly DBConnection _conn;
        public readonly PartialTimesDto PartialTimes;
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        public SelectPartialTimes(ref DBConnection conn, AthleteDto athlete, CompetitionDto competition) {
            _conn = conn;
            PartialTimes = new PartialTimesDto();
            _athlete = athlete;
            _competition = competition;
        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_PARTIAL_TIMES, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("COMPETITION_ID", _competition.ID);

                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        PartialTimes.CompetitionDto = _competition;
                        PartialTimes.Athlete = _athlete;
                        PartialTimes.Time = new long[_competition.NumberMilestone];
                        int count = 0;
                        while (reader.Read()) {
                            PartialTimes.Time[count] = reader.GetInt64(2);
                            count++;
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
