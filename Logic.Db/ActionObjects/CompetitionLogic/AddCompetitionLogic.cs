using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class AddCompetitionLogic : IActionObject
    {
        private readonly CompetitionDto _competition;


        private readonly DBConnection _conn;


        public AddCompetitionLogic(ref DBConnection conn, CompetitionDto competitionP) {
            _competition = competitionP;
            _conn = conn;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_INSERT_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_NAME", _competition.Name);
                    command.Parameters.AddWithValue("@COMPETITION_KM", _competition.Km);
                    //command.Parameters.AddWithValue("@COMPETITION_PRICE", _competition.Price);
                    command.Parameters.AddWithValue("@COMPETITION_DATE", _competition.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@COMPETITION_STATUS", _competition.Status);
                    command.Parameters.AddWithValue("@COMPETITION_NUMBER_PLACES", _competition.NumberPlaces);
                    command.Parameters.AddWithValue("@COMPETITION_RULES", _competition.Rules);

                    char tipo;
                    if (_competition.Type.Equals(TypeCompetition.Asphalt))
                        tipo = 'S';
                    else
                        tipo = 'M';

                    command.Parameters.AddWithValue("@COMPETITION_TYPE", tipo.ToString());

                    command.Parameters.AddWithValue("@COMPETITION_KM", _competition.Km);
                    command.Parameters.AddWithValue("@COMPETITION_DATE", _competition.Date.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@COMPETITION_NUMBER_PLACES", _competition.NumberPlaces);


                    command.Parameters.AddWithValue("@COMPETITION_STATUS", _competition.Status);

                    command.Parameters.AddWithValue("@COMPETITION_RULES", _competition.Rules);
                    command.Parameters.AddWithValue("@COMPETITION_SLOPE", _competition.Slope);
                    command.Parameters.AddWithValue("@COMPETITION_MILESTONES", _competition.NumberMilestone);
                    command.Parameters.AddWithValue("@COMPETITION_PREINSCRIPTION", _competition.Preinscription);
                    command.Parameters.AddWithValue("@COMPETITION_DAYS_PREINSCRIPTION",
                        _competition.DaysPreinscription);

                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
