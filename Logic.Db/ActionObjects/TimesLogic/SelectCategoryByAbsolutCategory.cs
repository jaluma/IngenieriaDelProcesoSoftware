using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.TimesLogic {
    public class SelectCategoryByAbsolutCategory :IActionObject {

        private readonly int[] _id;
        private readonly DBConnection _conn;

        public CategoryDto[] Categories;

        public SelectCategoryByAbsolutCategory(ref DBConnection conn, int[] id)
        {
            _conn = conn;
            _id =id;
        }

        public void Execute() {
            int count = 0;
            Categories = new CategoryDto[2];
            while (count < Categories.Length) {
                using (SQLiteCommand command1 = new SQLiteCommand(Properties.Resources.SQL_SELECT_CATEGORIES,
                    _conn.DbConnection)) {
                    command1.Parameters.AddWithValue("@ID", _id[count]);
                    using (SQLiteDataReader reader1 = command1.ExecuteReader()) {
                        while (reader1.Read()) {
                            CategoryDto category2 = new CategoryDto() {
                                Id = reader1.GetInt32(0),
                                Name = reader1.GetString(1),
                                MinAge = reader1.GetInt32(2),
                                MaxAge = reader1.GetInt32(3),
                                Gender = reader1.GetString(4)
                            };
                            Categories[count] = category2;
                        }
                    }
                }

                count++;
            }
        }
    }
}
