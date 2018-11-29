using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    internal class UpdateEnrollRefund : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly string _dni;
        private readonly long _id;
        private readonly double _refund;

        public UpdateEnrollRefund(ref DBConnection conn, string dni, long id, double refund) {
            _conn = conn;
            _dni = dni;
            _id = id;
            _refund = refund;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_UPDATE_ENROLL_REFUND, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@REFUND", _refund);
                    command.Parameters.AddWithValue("@DNI", _dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _id);
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
