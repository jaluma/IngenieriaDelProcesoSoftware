using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.TimesLogic
{
    public class SelectHasParticipatedTimeLogic : IActionObject
    {
        private readonly CompetitionDto _competition;
        private readonly DBConnection _conn;
        public readonly DataTable Table;

        public SelectHasParticipatedTimeLogic(ref DBConnection conn, CompetitionDto competition) {
            _conn = conn;
            Table = new DataTable();
            _competition = competition;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_ATHLETES_TIMES, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);

                    var da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
