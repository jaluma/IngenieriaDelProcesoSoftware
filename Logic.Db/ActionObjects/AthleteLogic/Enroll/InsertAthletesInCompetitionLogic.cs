using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {
    public class InsertAthletesInCompetitionLogic : IActionObject {

        private readonly DBConnection _conn;
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        public InsertAthletesInCompetitionLogic(ref DBConnection conn, AthleteDto athlete, CompetitionDto competition) {
            _conn = conn;
            _athlete = athlete;
            _competition = competition;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command =
                    new SQLiteCommand(Properties.Resources.SQL_INSERT_ENROLL, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni.ToUpper());
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    command.Parameters.AddWithValue("@STATUS", TypesStatus.Outstanding.ToString().ToUpper());
                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                //throw new ApplicationException();
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
