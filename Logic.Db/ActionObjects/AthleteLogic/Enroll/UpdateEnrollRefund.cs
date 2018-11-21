using Logic.Db.Connection;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    class UpdateEnrollRefund : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly string _dni;
        private readonly long _id;
        private readonly double _refund;

        public UpdateEnrollRefund(ref DBConnection conn, string dni, long id, double refund)
        {
            _conn = conn;
            _dni = dni;
            _id = id;
            _refund = refund;
        }

        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_UPDATE_ENROLL_REFUND, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@REFUND", _refund);
                    command.Parameters.AddWithValue("@DNI", _dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _id);
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException)
            {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
