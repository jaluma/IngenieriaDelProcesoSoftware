using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class CompetitionToInscribeClubs : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly int _count;
        public readonly DataTable Table;
        public List<CompetitionDto> Competitions;

        public CompetitionToInscribeClubs(ref DBConnection conn, int count) {
            _conn = conn;
            Table = new DataTable();
            _count = count;
            Competitions = new List<CompetitionDto>();
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_COMPETITIONS_TO_INSCRIBE_CLUBS,
                    _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COUNT", _count);
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
