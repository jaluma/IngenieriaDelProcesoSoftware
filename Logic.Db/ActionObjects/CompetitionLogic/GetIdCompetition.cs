using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CompetitionLogic {
    class GetIdCompetition : IActionObject {
        private readonly DBConnection _conn;
        private readonly CompetitionDto _competitionDto;
        public int id;

        public GetIdCompetition(ref DBConnection conn, CompetitionDto comp) {
            _conn = conn;
            _competitionDto = comp;
        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_GET_COMPETITION_ID, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_NAME", _competitionDto.Name);
                    //command.Parameters.AddWithValue("@COMPETITION_TYPE", _competitionDto.Type);
                    //command.Parameters.AddWithValue("@COMPETITION_KM", _competitionDto.Km);
                    command.Parameters.AddWithValue("@COMPETITION_DATE", _competitionDto.Date.ToString("yyyy-MM-dd"));
                    //command.Parameters.AddWithValue("@COMPETITION_NUMBER", _competitionDto.NumberPlaces);
                    //command.Parameters.AddWithValue("@COMPETITION_STATUS", _competitionDto.Status);
                    //command.Parameters.AddWithValue("@COMPETITION_RULES", _competitionDto.Rules);
                    //command.Parameters.AddWithValue("@COMPETITION_SLOPE", _competitionDto.Slope);
                    //command.Parameters.AddWithValue("@COMPETITION_NUMBER_MILESTONE", _competitionDto.Milestone);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            id = reader.GetInt32(0);
                        }
                    }
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
