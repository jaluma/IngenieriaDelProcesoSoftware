using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {
    class SelectNotCanceledInscriptions : IActionObject {

        private readonly DBConnection _conn;
        public readonly DataTable Table;
        private readonly string _dni;

        public SelectNotCanceledInscriptions(ref DBConnection conn, string dni) {
            _conn = conn;
            Table = new DataTable();
            _dni = dni;
        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_NOT_CANCELED_INSCRIPTIONS, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _dni);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
