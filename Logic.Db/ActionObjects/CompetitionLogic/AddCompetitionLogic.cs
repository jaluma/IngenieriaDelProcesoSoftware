using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CompetitionLogic {
    public class AddCompetitionLogic : IActionObject {

       
        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
       

        public AddCompetitionLogic(ref DBConnection conn, CompetitionDto competitionP) {
            _competition = competitionP;
            _conn = conn;
           
        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_COMPETITION, _conn.DbConnection)) {
                    
                    command.Parameters.AddWithValue("@COMPETITION_NAME", _competition.Name);
                    command.Parameters.AddWithValue("@COMPETITION_KM", _competition.Km);
                    command.Parameters.AddWithValue("@COMPETITION_PRICE", _competition.Price);
                    command.Parameters.AddWithValue("@COMPETITION_DATE", _competition.Date);
                    command.Parameters.AddWithValue("@COMPETITION_STATUS", _competition.Status);
                    command.Parameters.AddWithValue("@COMPETITION_NUMBER_PLACES", _competition.NumberPlaces);
                    command.Parameters.AddWithValue("@COMPETITION_RULES", _competition.Rules);

                    Char tipo;
                    if (_competition.Type.Equals(TypeCompetition.Asphalt))
                        tipo = 'S';
                    else
                        tipo = 'M';

                    command.Parameters.AddWithValue("@COMPETITION_TYPE", tipo.ToString());

                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }

    }
}
