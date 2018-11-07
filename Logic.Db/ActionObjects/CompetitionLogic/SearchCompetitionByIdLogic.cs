using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class SearchCompetitionByIdLogic : IActionObject
    {
        private DBConnection _conn;
        private readonly CompetitionDto _competitionDto;
        public CompetitionDto Competition;

        public SearchCompetitionByIdLogic(ref DBConnection conn, CompetitionDto comp)
        {
            _conn = conn;
            _competitionDto = comp;
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_SELECT_COMPETITION_BY_ID, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@ID", _competitionDto.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Competition = new CompetitionDto()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Km = reader.GetInt32(3),
                                //Price = reader.GetDouble(4),
                                Date = reader.GetDateTime(4),
                                Rules = new GetRulesCompetitionLogic(ref _conn, _competitionDto).Execute(),
                                Status = reader.GetString(6),
                                NumberMilestone = reader.GetInt32(7),
                                //Slope = reader.GetDouble(8)
                            };
                            Enum.TryParse<TypeCompetition>(reader.GetString(2), out Competition.Type);
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
