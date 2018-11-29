using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class SelectAthleteEnrollLogic : IActionObject
    {
        private readonly CompetitionDto _competition;

        private readonly DBConnection _conn;
        public readonly DataTable Table;

        public SelectAthleteEnrollLogic(ref DBConnection conn, CompetitionDto competitionP) {
            _conn = conn;
            _competition = competitionP;
            Table = new DataTable();
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_ATHLETE_INSCRIPTION, _conn.DbConnection)) {
                    //command.Parameters.AddWithValue("@STATUS", TypesStatus.Registered.ToString().ToUpper());
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    var da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
