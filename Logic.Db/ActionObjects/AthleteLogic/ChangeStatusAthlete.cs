using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Util;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    class ChangeStatusAthlete
    {
       
        private readonly DBConnection _conn;
        private readonly long _id;
        private readonly AthleteDto _athlete;

        public ChangeStatusAthlete(ref DBConnection conn, AthleteDto athleteP, long id)
        {
            _athlete = athleteP;
            _conn = conn;
            _id = id;
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_CHANGE_ATHLETE_STATUS, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _id);
                    command.Parameters.AddWithValue("@STATUS", "OUTSTANDING");
                   
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
