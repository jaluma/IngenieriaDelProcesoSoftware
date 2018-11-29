namespace Logic.Db.Csv.Object
{
    public class PartialTimesObjects : CsvObject
    {
        public int Dorsal { get; set; }
        public long CompetitionId { get; set; }
        public long[] Times { get; set; }
    }
}
