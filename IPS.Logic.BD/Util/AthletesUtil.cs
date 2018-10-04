using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.Util {
    public static class AthletesUtil {

        public static void InsertAthletesTable(ref DBConnection conn, AthleteDto athleteP) {
            try {
                SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_ATHLETE, conn.DbConnection);
                command.Parameters.AddWithValue("@DNI", athleteP.Dni);
                command.Parameters.AddWithValue("@NAME", athleteP.Name);
                command.Parameters.AddWithValue("@SURNAME", athleteP.Surname);
                command.Parameters.AddWithValue("@BIRTH_DATE", athleteP.BirthDate);
                command.Parameters.AddWithValue("@GENDER", athleteP.Gender);
                command.ExecuteNonQuery();
            } catch (SQLiteException) {
                conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
