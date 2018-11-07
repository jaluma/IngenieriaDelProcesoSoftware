using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {

    public class SelectDniFromDorsal : IActionObject {
        private readonly DBConnection _conn;
        private readonly long _competitionId;
        private readonly int _dorsal;
        public string Dni;

        public SelectDniFromDorsal(ref DBConnection conn, long competitionId, int dorsal) {
            _conn = conn;
            _competitionId = competitionId;
            _dorsal = dorsal;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_DNI_FROM_DORSAL, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DORSAL", _dorsal);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competitionId);

                    using (SQLiteDataReader reader = command.ExecuteReader()) {

                        while (reader.Read()) {
                            Dni = reader.GetString(0);
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
