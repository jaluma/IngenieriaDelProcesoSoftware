using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class SelectCategoryByAthleteAndCompetition : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;
        private readonly DBConnection _conn;
        public CategoryDto Category;

        public SelectCategoryByAthleteAndCompetition(ref DBConnection conn, CompetitionDto competition,
            AthleteDto athlete) {
            _conn = conn;
            _competition = competition;
            _athlete = athlete;
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_CATEGORY_BY_COMP_AND_ATHLETE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        reader.Read();

                        Category = new CategoryDto {
                            Id = reader.GetInt64(0),
                            Name = reader.GetString(1),
                            MinAge = reader.GetInt32(2),
                            MaxAge = reader.GetInt32(3),
                            Gender = reader.GetString(4)
                        };
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
