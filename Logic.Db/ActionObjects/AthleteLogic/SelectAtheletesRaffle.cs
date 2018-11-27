using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    class SelectAtheletesRaffle
    {
        private readonly DBConnection _conn;
        private readonly long _competitionID;
        public readonly List<AthleteDto> atletas;
        public AthleteDto Athlete;
        public  int numeroPlazas;


        public SelectAtheletesRaffle(ref DBConnection conn, long competitionID)
        {
            _conn = conn;
            _competitionID = competitionID;
            atletas = new List<AthleteDto>();


        }

        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETES_PREINSCRIPTED, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competitionID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Athlete = new AthleteDto()
                            {
                                Dni = reader.GetString(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2),
                               
                            };
                            numeroPlazas = reader.GetInt16(3);

                            atletas.Add(Athlete);
                        }
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
