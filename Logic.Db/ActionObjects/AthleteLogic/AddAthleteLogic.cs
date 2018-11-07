using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Util;

namespace Logic.Db.ActionObjects.AthleteLogic {
    public class AddAthleteLogic : IActionObject {

        //public Athlete AthleteAdd { get; private set; }
        private readonly DBConnection _conn;
        private readonly AthleteDto _athleteAdd;

        public AddAthleteLogic(ref DBConnection conn, AthleteDto athleteP) {
            _athleteAdd = athleteP;
            _conn = conn;
        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_ATHLETE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athleteAdd.Dni);
                    command.Parameters.AddWithValue("@NAME", _athleteAdd.Name);
                    command.Parameters.AddWithValue("@SURNAME", _athleteAdd.Surname);
                    command.Parameters.AddWithValue("@BIRTH_DATE", _athleteAdd.BirthDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@GENDER", new string((char)_athleteAdd.Gender, 1));
                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }

    }
}
