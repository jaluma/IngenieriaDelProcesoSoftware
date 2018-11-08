using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
namespace Logic.Db.ActionObjects.CompetitionLogic {
    class EnrollRefundsCompetition : IActionObject {


        private readonly DBConnection _conn;
        private readonly long _competition;
        private readonly double _refund;
        private readonly DateTime _date;



        public EnrollRefundsCompetition(ref DBConnection conn, long competitionID, DateTime date, double refund) {
            _competition = competitionID;
            _date = date;
            _conn = conn;
            _refund = refund;

        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_ENROLL_REFUNDS_COMPETITION, _conn.DbConnection)) {

                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition);
                    command.Parameters.AddWithValue("@DATE_REFUND", _date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@REFUND", _refund);

                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }

    }
}
