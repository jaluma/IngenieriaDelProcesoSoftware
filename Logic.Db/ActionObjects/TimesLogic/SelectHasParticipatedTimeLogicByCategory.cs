using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.TimesLogic {

    public class SelectHasParticipatedTimeLogicByCategory : IActionObject {
        private readonly DBConnection _conn;
        public readonly DataTable Table;
        private readonly CompetitionDto _competition;
        private readonly CategoryDto _category;

        public SelectHasParticipatedTimeLogicByCategory(ref DBConnection conn, CompetitionDto competition, CategoryDto categorySelected) {
            _conn = conn;
            Table = new DataTable();
            _competition = competition;
            _category = categorySelected;
        }
        public void Execute() {
            try {
                // sin genero
                if (_category.Gender == null) {
                    using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETES_TIMES, _conn.DbConnection)) {
                        command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                        command.Parameters.AddWithValue("@CATEGORY_MIN_AGE", _category.MinAge);
                        command.Parameters.AddWithValue("@CATEGORY_MAX_AGE", _category.MaxAge);

                        SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                        da.Fill(Table);
                    }
                } else {
                    using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETES_TIMES_BY_GENDER, _conn.DbConnection)) {
                        command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                        command.Parameters.AddWithValue("@GENDER", _category.Gender);
                        command.Parameters.AddWithValue("@CATEGORY_MIN_AGE", _category.MinAge);
                        command.Parameters.AddWithValue("@CATEGORY_MAX_AGE", _category.MaxAge);

                        SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                        da.Fill(Table);
                    }
                }
               
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
