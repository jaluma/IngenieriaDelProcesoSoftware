using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.TimesLogic {
    public class InsertPartialTimes {

        private readonly DBConnection _conn;
        private readonly PartialTimesDto _partial;
        private readonly string _dni;


        public InsertPartialTimes(ref DBConnection conn, string dni, PartialTimesDto partial) {
            _partial = partial;
            _dni = dni;
            _conn = conn;

        }
        public void Execute() {
            SQLiteConnection conn = null;
            try {
                conn = _conn.DbConnection;
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_PARTIAL_TIMES, conn)) {
                    command.Parameters.AddWithValue("@DNI", _dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _partial.CompetitionDto.ID);
                    int index = 1;
                    foreach (var time in _partial.Time) {
                        command.Parameters.AddWithValue("@MILESTONE", index++);
                        command.Parameters.AddWithValue("@time", time);
                        command.ExecuteNonQuery();
                    }

                }
            } catch (SQLiteException) {
                //conn.Close();
                throw new InvalidOperationException();
            }
        }
    }
}
