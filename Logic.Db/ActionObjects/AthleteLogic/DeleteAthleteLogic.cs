using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic {
    public class DeleteAthleteLogic : IActionObject {
        private readonly DBConnection _conn;
        private readonly AthleteDto _athlete;
        public DeleteAthleteLogic(ref DBConnection conn, AthleteDto athleteP) {
            _conn = conn;
            _athlete = athleteP;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_DELETE_ATHLETE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
