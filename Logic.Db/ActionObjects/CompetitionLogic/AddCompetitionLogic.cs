using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CompetitionLogic {
    public class AddCompetitionLogic : IActionObject {

        //public Athlete AthleteAdd { get; private set; }
        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;

        public AddCompetitionLogic(ref DBConnection conn, CompetitionDto competitionP) {
            _competition = competitionP;
            _conn = conn;
        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_COMPETITION, _conn.DbConnection)) {
                    //command.Parameters.AddWithValue("@ID", _competition.ID);

                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }

    }
}
