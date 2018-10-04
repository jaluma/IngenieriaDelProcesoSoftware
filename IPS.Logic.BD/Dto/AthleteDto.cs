using System.Data.SQLite;

namespace Logic.Db.Dto {
    public class AthleteDto {
        public string Dni;
        public string Name;
        public string Surname;
        public SQLiteDateFormats BirthDate;
        public Gender Gender;

    }

    public enum Gender {
        Male = 'M', Female = 'F'
    }
}
