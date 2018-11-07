using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CategoriesLogic
{
    class GetAbsoluteCategory : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly AbsoluteCategory _absolute;
        public int id;

        public GetAbsoluteCategory(ref DBConnection conn, AbsoluteCategory comp)
        {
            _conn = conn;
            _absolute = comp;
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_GET_ABSOLUTE_ID, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@NAME", _absolute.Name);
                    command.Parameters.AddWithValue("@ID_M", _absolute.CategoryM.Id);
                    command.Parameters.AddWithValue("@ID_F", _absolute.CategoryF.Id);


                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                    }
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
