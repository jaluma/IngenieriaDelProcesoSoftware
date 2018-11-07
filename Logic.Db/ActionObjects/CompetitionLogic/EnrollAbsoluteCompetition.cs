using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    class EnrollAbsoluteCompetition : IActionObject
    {


        private readonly DBConnection _conn;
        private readonly long _competition;
        private readonly long _date;



        public EnrollAbsoluteCompetition(ref DBConnection conn, long competitionID, long absoluteID)
        {
            _competition = competitionID;
            _date = absoluteID;
            _conn = conn;

        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_ENROLL_ABSOLUTE_COMPETITION, _conn.DbConnection))
                {

                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition);
                    command.Parameters.AddWithValue("@ABSOLUT_CATEGORY_ID", _date);
                    
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
