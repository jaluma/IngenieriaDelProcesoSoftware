using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto {

    public class HasParticipatedDto {
        public CompetitionDto Competition;
        public AthleteDto Athlete;
        public long InitialTime;
        public long FinishTime;
    }
}
