using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.AthleteLogic {
    public class CountAthleteByDniLogic : IActionObject {
        private readonly DBConnection _conn;
        private readonly string _dni;

        public int Contador;

        public CountAthleteByDniLogic(ref DBConnection conn, string dni) {
            _conn = conn;
            _dni = dni;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_COUNT_ATHLETE_BY_DNI, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _dni);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            Contador = reader.GetInt32(0);
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
