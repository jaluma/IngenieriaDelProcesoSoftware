using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CategoriesLogic
{
    class AddAbsoluteCategory : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly AbsoluteCategory _absolute;


        public AddAbsoluteCategory(ref DBConnection conn, AbsoluteCategory category)
        {
            _absolute = category;
            _conn = conn;

        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_ABSOLUTE, _conn.DbConnection))
                {

                    command.Parameters.AddWithValue("@NAME", _absolute.Name);
                    command.Parameters.AddWithValue("@ID_M", _absolute.CategoryM.Id);
                    command.Parameters.AddWithValue("@ID_F", _absolute.CategoryF.Id);
                    


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

