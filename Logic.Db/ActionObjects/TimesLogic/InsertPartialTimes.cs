using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.TimesLogic
{
    public class InsertPartialTimes
    {
        private readonly DBConnection _conn;
        private readonly string _dni;
        private readonly PartialTimesDto _partial;


        public InsertPartialTimes(ref DBConnection conn, string dni, PartialTimesDto partial) {
            _partial = partial;
            _dni = dni;
            _conn = conn;
        }

        public void Execute() {
            SQLiteConnection conn = null;
            try {
                conn = _conn.DbConnection;
                using (var command = new SQLiteCommand(Resources.SQL_INSERT_PARTIAL_TIMES, conn)) {
                    command.Parameters.AddWithValue("@DNI", _dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _partial.CompetitionDto.ID);
                    for (var i = 1; i < _partial.Time.Length - 1; i++) {
                        var time = _partial.Time[i];
                        command.Parameters.AddWithValue("@MILESTONE", i);
                        command.Parameters.AddWithValue("@TIME", time);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException) {
                //conn.Close();
                throw new InvalidOperationException();
            }
        }
    }
}
