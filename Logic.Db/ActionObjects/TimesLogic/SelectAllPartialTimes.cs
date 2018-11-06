using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Util.Services;

namespace Logic.Db.ActionObjects.TimesLogic {
    class SelectAllPartialTimes : IActionObject {

        private DBConnection _conn;
        private readonly CompetitionDto _competition;
        public IList<PartialTimesDto> List;

        public SelectAllPartialTimes(ref DBConnection conn, CompetitionDto competitionP) {
            _conn = conn;
            _competition = competitionP;
            List = new List<PartialTimesDto>();
        }

        public void Execute() {
            foreach (var athlete in new EnrollService(_competition).SelectAthleteHasParticipated()) {
                SelectPartialTimes partial = new SelectPartialTimes(ref _conn, athlete, _competition);
                partial.Execute();
                List.Add(partial.PartialTimes);
            }
        }
    }
}
