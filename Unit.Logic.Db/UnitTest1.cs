using System;
using Logic.Db.ActionObjects;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit.Logic.Db {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestAddAthletes() {

            AthleteDto athlete = new AthleteDto {
                Dni = "55555554D",
                Name = "Alejandro",
                Surname = "Perez",
                BirthDate = new DateTime(2010, 09, 11),
                Gender = Gender.Male
            };

            DBConnection db = new DBConnection();
            
            AthletesService service = new AthletesService(ref db);
            AthletesService.PrintAthletes(service.SelectAthleteTable());
        }
    }
}
