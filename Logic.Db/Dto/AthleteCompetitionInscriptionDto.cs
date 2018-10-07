using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto {
    public class AthleteCompetitionInscriptionDto {
        public AthleteDto Athlete;
        public CompetitionDto Competition;
        public TypesStatus Status;
    }

    public enum TypesStatus {
        PreRegistered,
        Registered,
        PendientPayment
    }
}
