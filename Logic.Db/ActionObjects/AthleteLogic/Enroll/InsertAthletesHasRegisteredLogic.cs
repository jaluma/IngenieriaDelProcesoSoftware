using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll
{
    public class InsertAthletesHasRegisteredLogic : IActionObject
    {
        private readonly DBConnection _conn;

        public InsertAthletesHasRegisteredLogic(ref DBConnection conn) {
            _conn = conn;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_INSERT_HAS_PARTICIPATED, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@STATUS", TypesStatus.Registered.ToString().ToUpper());
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
