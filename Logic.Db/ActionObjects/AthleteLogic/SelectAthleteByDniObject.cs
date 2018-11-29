using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    public class SelectAthleteByDniLogicObject
    {
        private readonly DBConnection _conn;
        private readonly string _dni;
        public AthleteDto Athlete;


        public SelectAthleteByDniLogicObject(ref DBConnection conn, string dni) {
            _conn = conn;
            _dni = dni;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_ATHLETE_BY_DNI, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _dni);

                    using (var reader = command.ExecuteReader()) {
                        reader.Read();
                        Athlete = new AthleteDto {
                            Dni = reader.GetString(0),
                            Name = reader.GetString(1),
                            Surname = reader.GetString(2)
                            //BirthDate = reader.GetDateTime(3),
                        };
                        if (reader.GetString(4).ToCharArray()[0].Equals(AthleteDto.MALE))
                            Athlete.Gender = AthleteDto.MALE;
                        else
                            Athlete.Gender = AthleteDto.FEMALE;
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
