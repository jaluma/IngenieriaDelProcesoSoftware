using System.Data.SQLite;
using System.IO;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    public class GetRulesCompetitionLogic
    {
        private readonly CompetitionDto _competition = new CompetitionDto();

        private readonly DBConnection _conn;


        public GetRulesCompetitionLogic(ref DBConnection conn, CompetitionDto com) {
            _conn = conn;
            _competition = com;
        }

        public byte[] Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_GET_RULES, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (var reader = command.ExecuteReader()) {
                        const int CHUNK_SIZE = 2 * 1024;
                        var buffer = new byte[CHUNK_SIZE];
                        long bytesRead;
                        long fieldOffset = 0;
                        using (var stream = new MemoryStream()) {
                            while (reader.Read())
                                if (reader.IsDBNull(0)) {
                                    return null;
                                }
                                else {
                                    while ((bytesRead = reader.GetBytes(0, fieldOffset, buffer, 0, buffer.Length)) >
                                           0) {
                                        stream.Write(buffer, 0, (int) bytesRead);
                                        fieldOffset += bytesRead;
                                    }


                                    return stream.ToArray();
                                }

                            return null;
                        }
                    }
                }
            }
            catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
