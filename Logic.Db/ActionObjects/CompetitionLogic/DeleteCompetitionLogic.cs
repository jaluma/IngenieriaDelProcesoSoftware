using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class DeleteCompetitionLogic : IActionObject
    {
        private readonly CompetitionDto _competition;
        private readonly DBConnection _conn;

        public DeleteCompetitionLogic(ref DBConnection conn, CompetitionDto competitionP) {
            _conn = conn;
            _competition = competitionP;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_DELETE_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@ID", _competition.ID);
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
