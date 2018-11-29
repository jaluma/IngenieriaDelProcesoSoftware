using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    internal class SelectNotCanceledInscriptions : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly string _dni;
        public readonly DataTable Table;

        public SelectNotCanceledInscriptions(ref DBConnection conn, string dni) {
            _conn = conn;
            Table = new DataTable();
            _dni = dni;
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_NOT_CANCELED_INSCRIPTIONS, _conn.DbConnection)) {
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
