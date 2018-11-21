using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.ActionObjects.TimesLogic;
using Logic.Db.Csv.Object;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services {
    public class TimesService : ServiceAdapter {

        public DataTable SelectCompetitionTimes(CompetitionDto competition) {
            SelectHasParticipatedTimeLogic selectCompetition = new SelectHasParticipatedTimeLogic(ref _conn, competition);
            selectCompetition.Execute();
            return selectCompetition.Table;
        }

        public HasParticipatedDto SelectCompetitionHasParticipated(CompetitionDto competition, AthleteDto athlete) {
            SelectHasParticipatedTimeObject selectCompetition = new SelectHasParticipatedTimeObject(ref _conn, competition, athlete);
            selectCompetition.Execute();
            return selectCompetition.HasParticipated;
        }

        public DataTable SelectCompetitionTimes(CompetitionDto competition, AbsoluteCategory categorySelected, string gender) {
            SelectHasParticipatedTimeLogicByCategory selectCompetition = new SelectHasParticipatedTimeLogicByCategory(ref _conn, competition, categorySelected, gender);
            selectCompetition.Execute();
            return selectCompetition.Table;
        }

        public IEnumerable<PartialTimesDto> SelectPartialTimes(CompetitionDto competition) {
            SelectAllPartialTimes partial = new SelectAllPartialTimes(ref _conn, competition);
            partial.Execute();
            return partial.List;
        }

        public PartialTimesDto SelectPartialTimesByAthlete(CompetitionDto competition, AthleteDto athlete) {
            SelectPartialTimes partial = new SelectPartialTimes(ref _conn, athlete, competition);
            partial.Execute();
            return partial.PartialTimes;
        }

        public void InsertPartialTime(string dni, PartialTimesDto partialTimes) {
            InsertPartialTimes insert = new InsertPartialTimes(ref _conn, dni, partialTimes);
            insert.Execute();
        }

        public void UpdateAthleteRegisteredDorsal(string dni, PartialTimesDto noInsert) {
            UpdatePartialTimes insert = new UpdatePartialTimes(ref _conn, dni, noInsert);
            insert.Execute();
        }
    }
}
