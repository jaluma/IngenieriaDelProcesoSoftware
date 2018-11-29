using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    internal class IsAthleteInCompetition : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        private readonly DBConnection _conn;
        public bool IsEnroll;

        public IsAthleteInCompetition(ref DBConnection conn, CompetitionDto competitionP, AthleteDto athlete) {
            _conn = conn;
            _competition = competitionP;
            _athlete = athlete;
            IsEnroll = false;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_IS_ATHLETE_IN_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        reader.Read();
                        if (reader.GetInt32(0) != 0)
                            IsEnroll = true;
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
