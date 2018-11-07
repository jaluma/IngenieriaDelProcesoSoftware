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
    class SearchCategoryChild : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly CategoryDto _child;
        public CategoryDto Child;

        public SearchCategoryChild(ref DBConnection conn, CategoryDto child)
        {
            _conn = conn;
            _child = child;
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_SELECT_CATEGORY_CHILD, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@CATEGORY_NAME", _child.Name);
                    command.Parameters.AddWithValue("@CATEGORY_MIN_AGE", _child.MinAge);
                    command.Parameters.AddWithValue("@CATEGORY_MAX_AGE", _child.MaxAge);
                    command.Parameters.AddWithValue("@GENDER", _child.Gender);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Child = new CategoryDto
                            {
                                Id= reader.GetInt32(0),
                                Name = reader.GetString(1),
                                MinAge = reader.GetInt32(2),
                                MaxAge = reader.GetInt32(3),
                                Gender= reader.GetString(4),
                                
                            };
                            
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

