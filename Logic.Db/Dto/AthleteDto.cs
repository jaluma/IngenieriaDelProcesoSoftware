using System;
using System.Data.SQLite;
using System.Xml;

namespace Logic.Db.Dto {
    public class AthleteDto {
        public string Dni;
        public string Name;
        public string Surname;
        public DateTime BirthDate;
        public char Gender;

        public const char MALE = 'M';
        public const char FEMALE = 'F';



        public override string ToString()
        {

            return this.Dni + " - " + this.Name + " " + this.Surname;

        }








    }

}
