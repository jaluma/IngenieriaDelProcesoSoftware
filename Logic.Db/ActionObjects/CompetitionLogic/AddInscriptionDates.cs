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


        public AddInscriptionDates(ref DBConnection conn, InscriptionDatesDto plazo)
        {
            _plazo= plazo;
            _conn = conn;

        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_INSCRIPTION_DATE, _conn.DbConnection))
                {

                    command.Parameters.AddWithValue("@INITIAL_DATE", _plazo.fechaInicio.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@FINISH_DATE", _plazo.fechaFin.ToString("yyyy-MM-dd"));                   

                    command.ExecuteNonQuery();
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

