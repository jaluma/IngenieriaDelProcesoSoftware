using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;


namespace Logic.Db.ActionObjects.CategoriesLogic {
    class AddCategoryLogic {
        private readonly DBConnection _conn;
        private readonly CategoryDto _category;


        public AddCategoryLogic(ref DBConnection conn, CategoryDto category) {
            _category = category;
            _conn = conn;

        }
        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_CATEGORY, _conn.DbConnection)) {

                    command.Parameters.AddWithValue("@CATEGORY_NAME", _category.Name);
                    command.Parameters.AddWithValue("@CATEGORY_MIN_AGE", _category.MinAge);
                    command.Parameters.AddWithValue("@CATEGORY_MAX_AGE", _category.MaxAge);
                    command.Parameters.AddWithValue("@GENDER", _category.Gender);


                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }

    }
}

