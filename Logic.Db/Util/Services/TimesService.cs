using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.ActionObjects.TimesLogic;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services {
    public class TimesService  : ServiceAdapter {

        private readonly CompetitionDto _competition;
        private readonly SelectHasParticipatedTimeLogic _selectCompetition;

        public TimesService(CompetitionDto competition, bool? male, bool? female) : base() {
            _competition = competition;
            _selectCompetition = new SelectHasParticipatedTimeLogic(ref _conn, _competition, male, female);
        }

        public DataTable SelectCompetitionTimes() {
            _selectCompetition.Execute();
            return _selectCompetition.Table;
        }

        public void SwitchMaleFilter() {
            _selectCompetition.MaleFilterSwitch();
        }

        public void SwitchFemaleFilter() {
            _selectCompetition.FemaleFilterSwitch();
        }
    }
}
