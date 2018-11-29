using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class GetCategoryLogic : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;
        private readonly DBConnection _conn;


        public GetCategoryLogic(ref DBConnection conn, CompetitionDto competitionP, AthleteDto athlete) {
            _conn = conn;
            _competition = competitionP;
            _athlete = athlete;
        }

        public string Category {
            get;
            private set;
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_CATEGORY_IN_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        if (!reader.HasRows) throw new SQLiteException();

                        reader.Read();
                        Category = reader.GetString(0);
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
