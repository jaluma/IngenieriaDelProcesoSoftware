using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    class CompetitionToInscribeClubs : IActionObject
    {
        private readonly DBConnection _conn;
        public readonly DataTable Table;
        public List<CompetitionDto> Competitions;
        private readonly int _count;

        public CompetitionToInscribeClubs(ref DBConnection conn, int count)
        {
            _conn = conn;
            Table = new DataTable();
            _count = count;
            Competitions = new List<CompetitionDto>();
        }
        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_COMPETITIONS_TO_INSCRIBE_CLUBS, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@COUNT", _count);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    da.Fill(Table);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CompetitionDto competitionDto = new CompetitionDto()
                            {
                                ID = reader.GetInt64(0),
                                Name = reader.GetString(1),
                                Km = reader.GetDouble(3),
                                Price = reader.GetDouble(4),
                            };
                            Competitions.Add(competitionDto);
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
