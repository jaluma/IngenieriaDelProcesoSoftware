using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto {
    public class PartialTimesDto {

        public AthleteDto Athlete;
        public CompetitionDto CompetitionDto;
        public long[] Time;
        public long InitialTime;
        public long FinishTime;
    }
}
