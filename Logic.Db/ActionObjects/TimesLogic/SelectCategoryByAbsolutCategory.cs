using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.TimesLogic
{
    public class SelectCategoryByAbsolutCategory : IActionObject
    {
        private readonly DBConnection _conn;

        private readonly int[] _id;

        public CategoryDto[] Categories;

        public SelectCategoryByAbsolutCategory(ref DBConnection conn, int[] id) {
            _conn = conn;
            _id = id;
        }

        public void Execute() {
            var count = 0;
            Categories = new CategoryDto[2];
            while (count < Categories.Length) {
                using (var command1 = new SQLiteCommand(Resources.SQL_SELECT_CATEGORIES,
                    _conn.DbConnection)) {
                    command1.Parameters.AddWithValue("@ID", _id[count]);
                    using (var reader1 = command1.ExecuteReader()) {
                        while (reader1.Read()) {
                            var category2 = new CategoryDto {
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
