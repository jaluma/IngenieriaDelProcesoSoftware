using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.TimesLogic {
    public class UpdatePartialTimes {

        private readonly DBConnection _conn;
        private readonly PartialTimesDto _partial;
        private readonly string _dni;
       

        public UpdatePartialTimes(ref DBConnection conn, string dni, PartialTimesDto partial) {
            _partial = partial;
            _dni = dni;
            _conn = conn;
           
        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_UPDATE_PARTIAL_TIMES, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _partial.CompetitionDto.ID);
                    int index = 1;
                    foreach (var time in _partial.Time) {
                        command.Parameters.AddWithValue("@MILESTONE", index++);
                        command.Parameters.AddWithValue("@TIME", time);
                        command.ExecuteNonQuery();
                    }

                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw new InvalidOperationException();
            }
        }
    }
}
