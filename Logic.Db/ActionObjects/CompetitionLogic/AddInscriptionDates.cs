using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;


namespace Logic.Db.ActionObjects.CompetitionLogic
{
    class AddInscriptionDates
    {

        private readonly DBConnection _conn;
        private readonly InscriptionDatesDto _plazo;
        private readonly CompetitionDto _competition;


        public AddInscriptionDates(ref DBConnection conn, InscriptionDatesDto plazo, CompetitionDto competition)
        {
            _plazo= plazo;
            _conn = conn;
            _competition = competition;

        }
        public void Execute()
        {
            string initial = _plazo.fechaInicio.ToString("yyyy-MM-dd");
            string finish = _plazo.fechaFin.ToString("yyyy-MM-dd");
            try {

                using (SQLiteCommand command =
                    new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_INSCRIPTION_DATE, _conn.DbConnection)) {

                    command.Parameters.AddWithValue("@INITIAL_DATE", initial);
                    command.Parameters.AddWithValue("@FINISH_DATE", finish);

                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException e)
            {
                if (e.ErrorCode != 19) {
                    _conn.DbConnection?.Close();
                    throw;
                }
                
            }

            try {
                using (SQLiteCommand command =
                    new SQLiteCommand(Logic.Db.Properties.Resources.SQL_ENROLL_COMPETITION_DATES, _conn.DbConnection)) {

                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition);
                    command.Parameters.AddWithValue("@INITIAL_DATE", initial);
                    command.Parameters.AddWithValue("@FINISH_DATE", finish);
                    command.Parameters.AddWithValue("@COMPETITION_PRICE", _plazo.precio);

                    command.ExecuteNonQuery();
                }

            } catch (SQLiteException e) {
                    _conn.DbConnection?.Close();
                    throw;
            }
        }


    }

}

