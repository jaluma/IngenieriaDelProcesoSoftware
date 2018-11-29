using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class IsCategoryInCompetitionLogic : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        private readonly DBConnection _conn;
        public bool IsCorrect;

        public IsCategoryInCompetitionLogic(ref DBConnection conn, CompetitionDto competitionP, AthleteDto athlete) {
            _conn = conn;
            _competition = competitionP;
            _athlete = athlete;
            IsCorrect = false;
        }

        public void Execute() {
            string category;
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_CATEGORY_IN_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        if (!reader.HasRows) throw new SQLiteException();

                        reader.Read();
                        category = reader.GetString(0);
                    }
                }

                using (var command = new SQLiteCommand(Resources.SQL_SELECT_COMPETITION_CATEGORY, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read())
                            if (category.Equals(reader.GetString(0))) {
                                IsCorrect = true;
                                return;
                            }
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
