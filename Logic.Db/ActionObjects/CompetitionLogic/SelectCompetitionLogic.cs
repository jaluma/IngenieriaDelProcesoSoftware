using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class SelectCompetitionLogic : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly List<CompetitionDto> List;

        public SelectCompetitionLogic(ref DBConnection conn) {
            _conn = conn;
            List = new List<CompetitionDto>();
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_COMPETITION, _conn.DbConnection)) {
                    using (var reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            var competition = new CompetitionDto {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Km = reader.GetInt32(3),
                                Price = reader.GetDouble(4),
                                Date = reader.GetDateTime(5),
                                NumberPlaces = reader.GetInt32(6),
                                Status = reader.GetString(7)
                            };
                            Enum.TryParse(reader.GetString(2), out competition.Type);
                            List.Add(competition);
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
