using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.TimesLogic
{
    public class SelectHasParticipatedTimeLogicByCategory : IActionObject
    {
        private readonly AbsoluteCategory _category;
        private readonly CompetitionDto _competition;
        private readonly DBConnection _conn;
        private readonly string _gender;
        public readonly DataTable Table;

        public SelectHasParticipatedTimeLogicByCategory(ref DBConnection conn, CompetitionDto competition,
            AbsoluteCategory categorySelected, string gender) {
            _conn = conn;
            Table = new DataTable();
            _competition = competition;
            _category = categorySelected;
            _gender = gender;
        }

        public void Execute() {
            try {
                if (_category.CategoryM.Id == 0 || _category.CategoryF.Id == 0)
                    using (var command = new SQLiteCommand(Resources.SQL_SELECT_ATHLETES_TIMES, _conn.DbConnection)) {
                        command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                        command.Parameters.AddWithValue("@CATEGORY_GENDER", _gender);

                        var da = new SQLiteDataAdapter(command);
                        da.Fill(Table);
                    }
                else
                    using (var command = new SQLiteCommand(Resources.SQL_SELECT_ATHLETES_TIMES_BY_GENDER,
                        _conn.DbConnection)) {
                        command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                        command.Parameters.AddWithValue("@CATEGORY_MIN_AGE_M", _category.CategoryM.MinAge);
                        command.Parameters.AddWithValue("@CATEGORY_MAX_AGE_M", _category.CategoryM.MaxAge);
                        command.Parameters.AddWithValue("@CATEGORY_MIN_AGE_F", _category.CategoryF.MinAge);
                        command.Parameters.AddWithValue("@CATEGORY_MAX_AGE_F", _category.CategoryF.MaxAge);
                        command.Parameters.AddWithValue("@CATEGORY_GENDER", _gender);

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
