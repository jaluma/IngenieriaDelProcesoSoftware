using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    class SelectAthleteByDniLogic
    {

        private readonly DBConnection _conn;
        private readonly string _dni;
        private AthleteDto _athlete;
        public readonly DataTable Table;


        public SelectAthleteByDniLogic(ref DBConnection conn, string dni)
        {
           _conn = conn;
           _dni = dni;
           Table = new DataTable();

        }

        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETE_BY_DNI, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@DNI", _dni);
                    
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

