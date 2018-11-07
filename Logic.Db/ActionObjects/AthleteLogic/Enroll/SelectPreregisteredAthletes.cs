using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class SelectPreregisteredAthletes : IActionObject
    {

        private readonly DBConnection _conn;
        public readonly List<PaymentDto> Preregistered;

        public SelectPreregisteredAthletes(ref DBConnection conn)
        {
            _conn = conn;
            Preregistered = new List<PaymentDto>();
        }

        public void Execute()
        {
            try
            {
                using (SQLiteCommand command =
                    new SQLiteCommand(Properties.Resources.SQL_SELECT_PREREGISTERED, _conn.DbConnection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PaymentDto payment = new PaymentDto()
                            {
                                Dni = reader.GetString(0),
                                Id = reader.GetInt32(1),
                                Amount = reader.GetDouble(2),
                                Date = reader.GetDateTime(3)
                            };
                            Preregistered.Add(payment);
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
