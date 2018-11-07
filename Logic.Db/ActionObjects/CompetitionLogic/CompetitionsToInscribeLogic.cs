using Logic.Db.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class CompetitionsToInscribeLogic : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly DataTable Table;
        public List<CompetitionDto> Competitions;
        private readonly AthleteDto _athlete;

        public CompetitionsToInscribeLogic(ref DBConnection conn, AthleteDto athlete)
        {
            _conn = conn;
            Table = new DataTable();
            _athlete = athlete;
            Competitions = new List<CompetitionDto>();
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_COMPETITION_TO_INSCRIBE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            CompetitionDto competitionDto = new CompetitionDto() {
                                ID = reader.GetInt64(0),
                                Name = reader.GetString(1),
                                Km = reader.GetDouble(3),
                                Price = reader.GetDouble(4),
                            };
                            Competitions.Add(competitionDto);
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
