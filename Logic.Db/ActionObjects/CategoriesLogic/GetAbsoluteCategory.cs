using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CategoriesLogic
{
    internal class GetAbsoluteCategory : IActionObject
    {
        private readonly AbsoluteCategory _absolute;
        private readonly DBConnection _conn;
        public int id;

        public GetAbsoluteCategory(ref DBConnection conn, AbsoluteCategory comp) {
            _conn = conn;
            _absolute = comp;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_GET_ABSOLUTE_ID, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@NAME", _absolute.Name);
                    command.Parameters.AddWithValue("@ID_M", _absolute.CategoryM.Id);
                    command.Parameters.AddWithValue("@ID_F", _absolute.CategoryF.Id);


                    using (var reader = command.ExecuteReader()) {
                        if (reader.Read()) id = reader.GetInt32(0);
                    }
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
