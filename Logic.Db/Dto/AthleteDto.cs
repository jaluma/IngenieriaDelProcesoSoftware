using System;

namespace Logic.Db.Dto
{
    public class AthleteDto
    {
        public const char MALE = 'M';
        public const char FEMALE = 'F';
        public DateTime BirthDate;
        public string Dni;
        public char Gender;
        public string Name;
        public string Surname;


        public override string ToString() {
            return Dni + " - " + Name + " " + Surname;
        }
    }
}
