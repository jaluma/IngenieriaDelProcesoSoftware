using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class SearchCompetitionByIdLogic : IActionObject
    {
        private readonly CompetitionDto _competitionDto;
        private DBConnection _conn;
        public CompetitionDto Competition;

        public SearchCompetitionByIdLogic(ref DBConnection conn, CompetitionDto comp) {
            _conn = conn;
            _competitionDto = comp;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_COMPETITION_BY_ID, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@ID", _competitionDto.ID);
                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            //Competition = new CompetitionDto()
                            //{
                            //    ID = reader.GetInt64(0),
                            //    Name = reader.GetString(1),
                            //    Km = reader.GetInt32(3),
                            //    Price = reader.GetDouble(4),
                            //    Date = reader.GetDateTime(5),
                            //    //Rules = new GetRulesCompetitionLogic(ref _conn, _competitionDto).Execute(),
                            //    Status = reader.GetString(7),
                            //    NumberMilestone = reader.GetInt32(8),
                            //    //Slope = reader.GetDouble(9)
                            //};

                            Competition = new CompetitionDto();
                            Competition.ID = reader.GetInt64(0);
                            Competition.Name = reader.GetString(1);
                            Competition.Km = reader.GetInt32(3);
                            Competition.Price = reader.GetDouble(4);
                            Competition.Date = reader.GetDateTime(5);
                            Competition.Rules = new GetRulesCompetitionLogic(ref _conn, _competitionDto).Execute();
                            Competition.Status = reader.GetString(7);
                            Competition.NumberMilestone = reader.GetInt32(8);
                            Competition.Slope = reader.IsDBNull(9) ? 0 : reader.GetDouble(9);
                            Competition.NumberPlaces = reader.GetInt32(10);
                            Competition.Preinscription = reader.GetBoolean(11);
                            Competition.DaysPreinscription = reader.GetInt32(12);
                            Enum.TryParse(reader.GetString(2), out Competition.Type);
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
