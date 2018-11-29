using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Properties;

namespace Logic.Db.ActionObjects.TimesLogic
{
    public class SelectPartialTimes : IActionObject
    {
        private readonly AthleteDto _athlete;
        private readonly CompetitionDto _competition;
        private readonly DBConnection _conn;
        public readonly PartialTimesDto PartialTimes;

        public SelectPartialTimes(ref DBConnection conn, AthleteDto athlete, CompetitionDto competition) {
            _conn = conn;
            PartialTimes = new PartialTimesDto();
            _athlete = athlete;
            _competition = competition;
        }

        public void Execute() {
            try {
                using (var command = new SQLiteCommand(Resources.SQL_SELECT_PARTIAL_TIMES, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("COMPETITION_ID", _competition.ID);

                    using (var reader = command.ExecuteReader()) {
                        PartialTimes.CompetitionDto = _competition;
                        PartialTimes.Athlete = _athlete;
                        PartialTimes.Time = new long[_competition.NumberMilestone];
                        var count = 0;
                        while (reader.Read() && count < _competition.NumberMilestone) {
                            PartialTimes.Dorsal = reader.GetInt32(1);
                            PartialTimes.Time[count] = reader.GetInt64(3);
                            PartialTimes.InitialTime = reader.GetInt32(4);
                            PartialTimes.FinishTime = reader.GetInt32(5);
                            count++;
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
