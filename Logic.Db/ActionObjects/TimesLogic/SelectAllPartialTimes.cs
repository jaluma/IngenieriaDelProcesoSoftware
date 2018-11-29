using System.Collections.Generic;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Logic.Db.ActionObjects.TimesLogic
{
    internal class SelectAllPartialTimes : IActionObject
    {
        private readonly CompetitionDto _competition;
        public readonly IList<PartialTimesDto> List;

        private DBConnection _conn;

        public SelectAllPartialTimes(ref DBConnection conn, CompetitionDto competitionP) {
            _conn = conn;
            _competition = competitionP;
            List = new List<PartialTimesDto>();
        }

        public void Execute() {
            foreach (var athlete in new EnrollService(_competition).SelectAthleteHasParticipated()) {
                var partial = new SelectPartialTimes(ref _conn, athlete, _competition);
                partial.Execute();
                List.Add(partial.PartialTimes);
            }
        }
    }
}
