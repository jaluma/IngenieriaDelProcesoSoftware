using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CategoriesLogic
{
    internal class AddAbsoluteCategory : IActionObject
    {
        private readonly AbsoluteCategory _absolute;
        private readonly DBConnection _conn;


        public AddAbsoluteCategory(ref DBConnection conn, AbsoluteCategory category) {
            _absolute = category;
            _conn = conn;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_INSERT_ABSOLUTE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@NAME", _absolute.Name);
                    command.Parameters.AddWithValue("@ID_M", _absolute.CategoryM.Id);
                    command.Parameters.AddWithValue("@ID_F", _absolute.CategoryF.Id);


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
