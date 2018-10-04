using System;
using System.Data.SQLite;

namespace Logic.Db.Dto {
    public class AthleteDto {
        public string Dni;
        public string Name;
        public string Surname;
        public DateTime BirthDate;
        public Gender Gender;

    }

    public enum Gender {
        Male = 'M', Female = 'F'
    }
}
