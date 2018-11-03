using Logic.Db.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    
        class SelectCategoriesPredefinied : IActionObject
        {
            private readonly DBConnection _conn;
            public readonly DataTable Table;

            public SelectCategoriesPredefinied(ref DBConnection conn)
            {
                _conn = conn;
                Table = new DataTable();
            }
            public void Execute()
            {
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ALL_CATEGORIES, _conn.DbConnection))
                    {
                        SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                        da.Fill(Table);
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

