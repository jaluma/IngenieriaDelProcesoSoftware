using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class CompetitionsToPreinscribeLogic : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly DBConnection _conn;
        public readonly DataTable Table;
        public List<CompetitionDto> Competitions;

        public CompetitionsToPreinscribeLogic(ref DBConnection conn, AthleteDto athlete) {
            _conn = conn;
            Table = new DataTable();
            _athlete = athlete;
            Competitions = new List<CompetitionDto>();
        }

        public void Execute() {
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_SELECT_COMPETITION_TO_PREINSCRIBE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    var da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var competitionDto = new CompetitionDto {
                                ID = reader.GetInt64(0),
                                Name = reader.GetString(1),
                                Km = reader.GetDouble(3),
                                Price = reader.GetDouble(4)
                            };
                            Competitions.Add(competitionDto);
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
