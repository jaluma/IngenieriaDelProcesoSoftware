using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    class ListNotRealizedCompetitionLogic : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly DataTable Table;

        public ListNotRealizedCompetitionLogic(ref DBConnection conn)
        {
            _conn = conn;
            Table = new DataTable();
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_COMPETITION_FINISH_LIST, _conn.DbConnection))
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




