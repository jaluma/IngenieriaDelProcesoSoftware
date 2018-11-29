using System.Collections.Generic;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class SelectOutstandingAthletes : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly List<PaymentDto> Preregistered;

        public SelectOutstandingAthletes(ref DBConnection conn) {
            _conn = conn;
            Preregistered = new List<PaymentDto>();
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_OUTSTANDING, _conn.DbConnection)) {
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var payment = new PaymentDto {
                                Dni = reader.GetString(0),
                                Id = reader.GetInt32(1),
                                Amount = reader.GetDouble(2),
                                Date = reader.GetDateTime(3)
                            };
                            Preregistered.Add(payment);
                        }
                    }
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
