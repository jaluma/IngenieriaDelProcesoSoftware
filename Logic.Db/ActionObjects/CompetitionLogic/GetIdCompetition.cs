using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class GetIdCompetition : IActionObject
    {
        private readonly CompetitionDto _competitionDto;
        private readonly DBConnection _conn;
        public int id;

        public GetIdCompetition(ref DBConnection conn, CompetitionDto comp) {
            _conn = conn;
            _competitionDto = comp;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_GET_COMPETITION_ID, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_NAME", _competitionDto.Name);
                    //command.Parameters.AddWithValue("@COMPETITION_TYPE", _competitionDto.Type);
                    //command.Parameters.AddWithValue("@COMPETITION_KM", _competitionDto.Km);
                    command.Parameters.AddWithValue("@COMPETITION_DATE", _competitionDto.Date.ToString("yyyy-MM-dd"));
                    //command.Parameters.AddWithValue("@COMPETITION_NUMBER", _competitionDto.NumberPlaces);
                    //command.Parameters.AddWithValue("@COMPETITION_STATUS", _competitionDto.Status);
                    //command.Parameters.AddWithValue("@COMPETITION_RULES", _competitionDto.Rules);
                    //command.Parameters.AddWithValue("@COMPETITION_SLOPE", _competitionDto.Slope);
                    //command.Parameters.AddWithValue("@COMPETITION_NUMBER_MILESTONE", _competitionDto.Milestone);
                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) id = reader.GetInt32(0);
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
