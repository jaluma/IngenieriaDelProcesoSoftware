using Logic.Db.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class CompetitionsToInscribeLogic : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly DataTable Table;
        private readonly AthleteDto _athlete;

        public CompetitionsToInscribeLogic(ref DBConnection conn, AthleteDto athlete)
        {
            _conn = conn;
            Table = new DataTable();
            _athlete = athlete;
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_COMPETITION_TO_INSCRIBE, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
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
