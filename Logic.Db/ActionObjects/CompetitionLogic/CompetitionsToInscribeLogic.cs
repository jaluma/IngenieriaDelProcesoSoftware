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
    public class CompetitionsToInscribeLogic : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly DataTable Table;

        public CompetitionsToInscribeLogic(ref DBConnection conn)
        {
            _conn = conn;
            Table = new DataTable();
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_COMPETITION_TO_INSCRIBE, _conn.DbConnection))
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
