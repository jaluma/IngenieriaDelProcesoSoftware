using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {
    public class SelectAthleteHasParticipated : IActionObject {
        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        public readonly List<AthleteDto> List;

        public SelectAthleteHasParticipated(ref DBConnection conn, CompetitionDto competition) {
            _conn = conn;
            _competition = competition;
            List = new List<AthleteDto>();
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETES_HAS_PARTICIPATED, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);

                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            AthleteDto athlete = new AthleteDto() {
                                Dni = reader.GetString(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2),
                                BirthDate = reader.GetDateTime(3),
                                Gender = reader.GetString(4).ToCharArray()[0]
                            };
                            List.Add(athlete);
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
