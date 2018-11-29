namespace Logic.Db.Dto
{
    public class AthleteCompetitionInscriptionDto
    {
        public AthleteDto Athlete;
        public CompetitionDto Competition;
        public TypesStatus Status;
    }

    public enum TypesStatus
    {
        PreRegistered,
        Outstanding,
        Registered
    }
}
