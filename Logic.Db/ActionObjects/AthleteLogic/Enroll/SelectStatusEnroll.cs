using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {

    public class SelectStatusEnroll : IActionObject {

        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        private readonly string _dni;
        public string Return;

        public SelectStatusEnroll(ref DBConnection conn, CompetitionDto competitionP, string dni) {
            _conn = conn;
            _dni = dni;
            _competition = competitionP;
        }

        public void Execute() {
            try {
                if (_competition == null || _dni == null) {
                    return;
                }
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_STATUS_ENROLL, _conn.DbConnection)) {
                    //command.Parameters.AddWithValue("@STATUS", TypesStatus.Registered.ToString().ToUpper());
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    command.Parameters.AddWithValue("@DNI", _dni);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            Return = reader.GetString(0);
                        }
                    }
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
