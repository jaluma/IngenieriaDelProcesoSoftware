using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services {
    public class EnrollService : ServiceAdapter {

        private readonly CompetitionDto _competition;

        public EnrollService(CompetitionDto competition) {
            _competition = competition;
        }

        public DataTable SelectAthleteRegistered() {
            SelectAthleteEnrollLogic select = new SelectAthleteEnrollLogic(ref _conn, _competition);
            select.Execute();
            return select.Table;
        }
    }
}
