using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.ActionObjects.TimesLogic;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services {
    public class TimesService  : ServiceAdapter {

        public DataTable SelectCompetitionTimes(CompetitionDto competition) {
            SelectHasParticipatedTimeLogic selectCompetition = new SelectHasParticipatedTimeLogic(ref _conn, competition);
            selectCompetition.Execute();
            return selectCompetition.Table;
        }
    }
}
