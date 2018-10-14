using System;
using Logic.Db.ActionObjects;
using Logic.Db.Connection;
using Logic.Db.Dto;
using Logic.Db.Util;
using Logic.Db.Util.Services;
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
            
            AthletesService service = new AthletesService();
            AthletesService.PrintAthletes(service.SelectAthleteTable());
        }
    }
}
