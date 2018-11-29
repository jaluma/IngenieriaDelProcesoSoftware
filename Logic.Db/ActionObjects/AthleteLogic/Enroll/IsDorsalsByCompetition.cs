using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class IsDorsalsByCompetition : IActionObject
    {
        private readonly CompetitionDto _competition;

        private readonly DBConnection _conn;
        public bool IsDorsals;

        public IsDorsalsByCompetition(ref DBConnection conn, CompetitionDto competitionP) {
            _conn = conn;
            _competition = competitionP;
            IsDorsals = false;
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_COUNT_DORSALS_BY_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        if (!reader.HasRows) throw new SQLiteException();

                        reader.Read();
                        IsDorsals = reader.GetInt32(0) != 0;
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
