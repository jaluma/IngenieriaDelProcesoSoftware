using System;

namespace Logic.Db.Dto
{
    public class CompetitionDto
    {
        public DateTime Date;
        public int DaysPreinscription;
        public long ID;
        public double Km;
        public string Name;
        public int NumberMilestone;
        public int NumberPlaces;
        public bool Preinscription;
        public double Price;
        public byte[] Rules;
        public double Slope;
        public string Status; //can be null
        public TypeCompetition Type;
    }

    public enum TypeCompetition
    {
        Mountain = 'M',
        Asphalt = 'S'
    }
}
