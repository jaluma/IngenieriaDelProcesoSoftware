using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CategoriesLogic
{
    internal class AddCategoryLogic
    {
        private readonly CategoryDto _category;
        private readonly DBConnection _conn;


        public AddCategoryLogic(ref DBConnection conn, CategoryDto category) {
            _category = category;
            _conn = conn;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_INSERT_CATEGORY, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@CATEGORY_NAME", _category.Name);
                    command.Parameters.AddWithValue("@CATEGORY_MIN_AGE", _category.MinAge);
                    command.Parameters.AddWithValue("@CATEGORY_MAX_AGE", _category.MaxAge);
                    command.Parameters.AddWithValue("@GENDER", _category.Gender);


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
