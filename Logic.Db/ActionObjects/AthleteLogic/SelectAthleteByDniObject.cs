using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    class SelectAthleteByDniLogicObject
    {

        private readonly DBConnection _conn;
        private readonly string _dni;
        public AthleteDto Athlete;


        public SelectAthleteByDniLogicObject(ref DBConnection conn, string dni)
        {
           _conn = conn;
           _dni = dni;

        }

        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETE_BY_DNI, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@DNI", _dni);

                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        Athlete = new AthleteDto() {
                            Dni = reader.GetString(0),
                            Name = reader.GetString(1),
                            Surname = reader.GetString(2),
                            BirthDate = reader.GetDateTime(3),
                            Gender = reader.GetString(4).ToCharArray()[0]
                        };
                    }

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

