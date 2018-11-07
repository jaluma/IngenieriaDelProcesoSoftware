using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class GetRulesCompetitionLogic
    {
       
            private readonly DBConnection _conn;
        private readonly CompetitionDto _competition =new CompetitionDto();


        public GetRulesCompetitionLogic(ref DBConnection conn, CompetitionDto com)
            {
                _conn = conn;
                _competition = com;
              

        }
        public byte[] Execute()
            {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_GET_RULES, _conn.DbConnection))
                {

                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        const int CHUNK_SIZE = 2 * 1024;
                        byte[] buffer = new byte[CHUNK_SIZE];
                        long bytesRead;
                        long fieldOffset = 0;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(0))
                                {
                                    return null;
                                }
                                else
                                {
                                    while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) > 0)
                                    {
                                        stream.Write(buffer, 0, (int)bytesRead);
                                        fieldOffset += bytesRead;
                                    }


                                    return stream.ToArray();
                                }
                            }
                            return null;
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

