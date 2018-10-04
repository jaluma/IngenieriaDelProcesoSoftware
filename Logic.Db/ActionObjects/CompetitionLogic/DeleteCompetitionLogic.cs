using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CompetitionLogic {
    class DeleteCompetitionLogic : IActionObject {
        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        public DeleteCompetitionLogic(ref DBConnection conn, CompetitionDto competitionP) {
            _conn = conn;
            _competition = competitionP;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_DELETE_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@ID", _competition.ID);
                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
