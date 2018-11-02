using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class SelectAllCategoriesByCompetitionId : IActionObject
    {
        private readonly DBConnection _conn;
        private readonly CompetitionDto _competitionDto;
        public IList<CategoryDto> Categories;

        public SelectAllCategoriesByCompetitionId(ref DBConnection conn, CompetitionDto comp)
        {
            _conn = conn;
            _competitionDto = comp;
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_SELECT_CATEGORY_BY_COMPETITION, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competitionDto.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        Categories = new List<CategoryDto>();
                        while (reader.Read()) {
                            CategoryDto category = new CategoryDto();

                            category.Name = reader.GetString(0);
                            category.MinAge = reader.GetInt32(1);

                            if (!reader.IsDBNull(2))
                                category.MaxAge = reader.GetInt32(2);
                            else {
                                category.MaxAge = -1;
                            }
                            category.Gender = reader.IsDBNull(3) ? null : reader.GetString(3);

                            Categories.Add(category);
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
