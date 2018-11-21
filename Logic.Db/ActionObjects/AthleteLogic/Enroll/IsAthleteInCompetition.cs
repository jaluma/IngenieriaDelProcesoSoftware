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
    class IsAthleteInCompetition : IActionObject
    {

        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        private readonly AthleteDto _athlete;
        public bool IsEnroll;

        public IsAthleteInCompetition(ref DBConnection conn, CompetitionDto competitionP, AthleteDto athlete)
        {
            _conn = conn;
            _competition = competitionP;
            _athlete = athlete;
            IsEnroll = false;
        }

        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_IS_ATHLETE_IN_COMPETITION, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@ID", _competition.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.GetInt32(0) != 0)
                            IsEnroll = true;
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
