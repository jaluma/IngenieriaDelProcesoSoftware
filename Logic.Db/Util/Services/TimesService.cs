using System.Collections.Generic;
using System.Data;
using Logic.Db.ActionObjects.TimesLogic;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services
{
    public class TimesService : ServiceAdapter
    {
        public DataTable SelectCompetitionTimes(CompetitionDto competition) {
            var selectCompetition = new SelectHasParticipatedTimeLogic(ref _conn, competition);
            selectCompetition.Execute();
            return selectCompetition.Table;
        }

        public HasParticipatedDto SelectCompetitionHasParticipated(CompetitionDto competition, AthleteDto athlete) {
            var selectCompetition = new SelectHasParticipatedTimeObject(ref _conn, competition, athlete);
            selectCompetition.Execute();
            return selectCompetition.HasParticipated;
        }

        public DataTable SelectCompetitionTimes(CompetitionDto competition, AbsoluteCategory categorySelected,
            string gender) {
            var selectCompetition =
                new SelectHasParticipatedTimeLogicByCategory(ref _conn, competition, categorySelected, gender);
            selectCompetition.Execute();
            return selectCompetition.Table;
        }

        public IEnumerable<PartialTimesDto> SelectPartialTimes(CompetitionDto competition) {
            var partial = new SelectAllPartialTimes(ref _conn, competition);
            partial.Execute();
            return partial.List;
        }

        public PartialTimesDto SelectPartialTimesByAthlete(CompetitionDto competition, AthleteDto athlete) {
            var partial = new SelectPartialTimes(ref _conn, athlete, competition);
            partial.Execute();
            return partial.PartialTimes;
        }

        public void InsertPartialTime(string dni, PartialTimesDto partialTimes) {
            var insert = new InsertPartialTimes(ref _conn, dni, partialTimes);
            insert.Execute();
        }

        public void UpdateAthleteRegisteredDorsal(string dni, PartialTimesDto noInsert) {
            var insert = new UpdatePartialTimes(ref _conn, dni, noInsert);
            insert.Execute();
        }
    }
}
