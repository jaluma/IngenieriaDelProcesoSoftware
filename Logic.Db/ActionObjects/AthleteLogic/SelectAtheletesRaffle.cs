using System.Collections.Generic;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    internal class SelectAtheletesRaffle
    {
        private readonly long _competitionID;
        private readonly DBConnection _conn;
        public readonly List<AthleteDto> atletas;
        public AthleteDto Athlete;
        public int numeroPlazas;


        public SelectAtheletesRaffle(ref DBConnection conn, long competitionID) {
            _conn = conn;
            _competitionID = competitionID;
            atletas = new List<AthleteDto>();
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_ATHLETES_PREINSCRIPTED, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competitionID);

                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            Athlete = new AthleteDto {
                                Dni = reader.GetString(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2)
                            };
                            numeroPlazas = reader.GetInt16(3);

                            atletas.Add(Athlete);
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
