using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic {

    public class InsertAthletesHasRegisteredLogic : IActionObject {

        private readonly DBConnection _conn;
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;
        public InsertAthletesHasRegisteredLogic(ref DBConnection conn, AthleteDto athleteP, CompetitionDto competitionP) {
            _conn = conn;
            _athlete = athleteP;
            _competition = competitionP;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETES_STATUS, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@STATUS", TypesStatus.Registered.ToString().ToUpper());
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        using (SQLiteCommand insert = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_HAS_PARTICIPATED,_conn.DbConnection)) {
                            int dorsals = 20;
                            while (reader.NextResult()) {
                                insert.Parameters.AddWithValue("@COMPETITION_ID", reader.GetInt32(1));
                                insert.Parameters.AddWithValue("@ATHLETE_DNI", reader.GetString(0));
                                insert.Parameters.AddWithValue("@INITIAL_TIME", "NULL");
                                insert.Parameters.AddWithValue("@FINISH_TIME", "NULL");
                                insert.Parameters.AddWithValue("@DORSAL", dorsals++);
                            }

                            insert.ExecuteNonQuery();
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
