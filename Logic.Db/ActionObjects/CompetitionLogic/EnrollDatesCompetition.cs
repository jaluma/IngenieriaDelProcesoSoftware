using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class EnrollDatesCompetition : IActionObject
    {
        private readonly long _competition;


        private readonly DBConnection _conn;
        private readonly InscriptionDatesDto _date;


        public EnrollDatesCompetition(ref DBConnection conn, long competitionID, InscriptionDatesDto date) {
            _competition = competitionID;
            _date = date;
            _conn = conn;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_ENROLL_COMPETITION_DATES, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition);
                    command.Parameters.AddWithValue("@INITIAL_DATE", _date.FechaInicio.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@FINISH_DATE", _date.FechaFin.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@COMPETITION_PRICE", _date.Devolucion);

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
