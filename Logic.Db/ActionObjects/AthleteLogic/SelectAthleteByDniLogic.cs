﻿using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    internal class SelectAthleteByDniLogic
    {
        private readonly DBConnection _conn;
        private readonly string _dni;
        public readonly DataTable Table;


        public SelectAthleteByDniLogic(ref DBConnection conn, string dni) {
            _conn = conn;
            _dni = dni;
            Table = new DataTable();
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_ATHLETE_BY_DNI, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _dni);

                    var da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
