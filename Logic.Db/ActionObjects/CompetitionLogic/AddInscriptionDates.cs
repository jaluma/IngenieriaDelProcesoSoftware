using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    internal class AddInscriptionDates
    {
        private readonly CompetitionDto _competition;

        private readonly DBConnection _conn;
        private readonly InscriptionDatesDto _plazo;


        public AddInscriptionDates(ref DBConnection conn, InscriptionDatesDto plazo, CompetitionDto competition) {
            _plazo = plazo;
            _conn = conn;
            _competition = competition;
        }

        public void Execute() {
            var initial = _plazo.FechaInicio.ToString("yyyy-MM-dd");
            var finish = _plazo.FechaFin.ToString("yyyy-MM-dd");
            long max = 0;
            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_MAX_ID_DATES, _conn.DbConnection)) {
                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) max = reader.GetInt64(0);
                    }
                }
            }
            catch (SQLiteException e) {
                if (e.ErrorCode != 19) {
                    _conn.DbConnection?.Close();
                    throw;
                }

                return;
            }

            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_INSERT_INSCRIPTION_DATE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@ID", max + 1);
                    command.Parameters.AddWithValue("@INITIAL_DATE", initial);
                    command.Parameters.AddWithValue("@FINISH_DATE", finish);

                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException e) {
                if (e.ErrorCode != 19) {
                    _conn.DbConnection?.Close();
                    throw;
                }

                return;
            }

            try {
                using (var command =
                    new SQLiteCommand(Resources.SQL_ENROLL_COMPETITION_DATES, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@ID", max + 1);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    command.Parameters.AddWithValue("@COMPETITION_PRICE", _plazo.precio);

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
