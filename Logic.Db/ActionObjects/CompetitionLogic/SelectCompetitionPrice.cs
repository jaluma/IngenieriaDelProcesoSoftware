using Logic.Db.Connection;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CompetitionLogic {
    class SelectCompetitionPrice : IActionObject {
        private readonly DBConnection _conn;
        private readonly string _dni;
        private readonly long _id;

        public double Price;

        public SelectCompetitionPrice(ref DBConnection conn, string dni, long id) {
            _conn = conn;
            _dni = dni;
            _id = id;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_COMPETITION_PRICE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@ID", _id);
                    command.Parameters.AddWithValue("@DNI", _dni);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        if (reader.Read())
                            Price = reader.GetDouble(0);
                    }
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
