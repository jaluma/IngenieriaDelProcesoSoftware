using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Dto.Types;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class InsertAthletesInCompetitionLogic : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;

        private readonly DBConnection _conn;
        private readonly TypesStatus _status;

        public InsertAthletesInCompetitionLogic(ref DBConnection conn, AthleteDto athlete, CompetitionDto competition,
            TypesStatus status) {
            _conn = conn;
            _athlete = athlete;
            _competition = competition;
            _status = status;
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_INSERT_ENROLL, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni.ToUpper());
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    command.Parameters.AddWithValue("@STATUS", _status.ToString().ToUpper());
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException) {
                //throw new ApplicationException();
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
