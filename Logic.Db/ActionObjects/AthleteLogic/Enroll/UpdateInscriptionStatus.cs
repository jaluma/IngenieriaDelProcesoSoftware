using Logic.Db.Connection;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {
    public class UpdateInscriptionStatus : IActionObject {
        private readonly DBConnection _conn;
        private readonly string _dni;
        private readonly long _id;
        private readonly string _status;

        public UpdateInscriptionStatus(ref DBConnection conn, string dni, long id, string status) {
            _conn = conn;
            _dni = dni;
            _id = id;
            _status = status;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_UPDATE_INSCRIPTION_STATUS, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@STATUS", _status);
                    command.Parameters.AddWithValue("@DNI", _dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _id);
                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
