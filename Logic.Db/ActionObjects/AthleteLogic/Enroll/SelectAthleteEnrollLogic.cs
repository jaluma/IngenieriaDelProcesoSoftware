using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {

    public class SelectAthleteEnrollLogic : IActionObject {

        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        public readonly DataTable Table;

        public SelectAthleteEnrollLogic(ref DBConnection conn, CompetitionDto competitionP) {
            _conn = conn;
            _competition = competitionP;
            Table = new DataTable();
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETE_INSCRIPTION, _conn.DbConnection)) {
                    //command.Parameters.AddWithValue("@STATUS", TypesStatus.Registered.ToString().ToUpper());
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
