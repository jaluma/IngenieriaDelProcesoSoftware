using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class EnrollRefundsCompetition : IActionObject
    {
        private readonly long _competition;


        private readonly DBConnection _conn;
        private readonly DateTime _date;
        private readonly double _refund;


        public EnrollRefundsCompetition(ref DBConnection conn, long competitionID, DateTime date, double refund) {
            _competition = competitionID;
            _date = date;
            _conn = conn;
            _refund = refund;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_ENROLL_REFUNDS_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition);
                    command.Parameters.AddWithValue("@DATE_REFUND", _date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@REFUND", _refund);

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
