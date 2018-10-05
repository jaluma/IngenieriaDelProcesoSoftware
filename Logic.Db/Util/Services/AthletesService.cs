using System;
using System.Collections.Generic;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services {
    public class AthletesService : ServiceAdapter {

        public void InsertAthletesTable(AthleteDto athleteP) {
            AddAthleteLogic add = new AddAthleteLogic(ref _conn, athleteP);
            add.Execute();
        }

        public List<AthleteDto> SelectAthleteTable() {
            SelectAthleteLogic select = new SelectAthleteLogic(ref _conn);
            select.Execute();
            return select.List;
        }

        public void DeleteAthleteTable(AthleteDto athleteP) {
            DeleteAthleteLogic delete = new DeleteAthleteLogic(ref _conn, athleteP);
            delete.Execute();
        }

        public static void PrintAthletes(IEnumerable<AthleteDto> list) {
            Console.WriteLine(string.Join("\n", list));
        }

        //public static void PrintAthletes(IEnumerable<Athlete> list) {
        //    Console.WriteLine(string.Join("\n", list));
        //}
    }
}
