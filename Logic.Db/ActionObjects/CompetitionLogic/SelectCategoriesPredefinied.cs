using Logic.Db.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using Logic.Db.Dto;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    
        class SelectCategoriesPredefinied : IActionObject
        {
            private readonly DBConnection _conn;
            public readonly List<CategoryDto> list;

            public SelectCategoriesPredefinied(ref DBConnection conn)
            {
                _conn = conn;
            list = new List<CategoryDto>();
            }
            public void Execute()
            {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ALL_CATEGORIES, _conn.DbConnection))

                using (SQLiteDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        CategoryDto cat = new CategoryDto()
                        {

                            Name = reader.GetString(0),
                            Min_Age = reader.GetInt16(1),
                            Max_Age = reader.GetInt16(1)
                    };

                        list.Add(cat);
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

