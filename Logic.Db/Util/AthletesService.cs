using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.Util {
    public class AthletesService {

        private DBConnection _conn;

        public AthletesService(ref DBConnection conn) {
            _conn = conn;
        }

        public void InsertAthletesTable(AthleteDto athleteP) {
            AddAthleteLogic add = new AddAthleteLogic(ref _conn, athleteP);
            add.Execute();
        }

        public List<AthleteDto> SelectAthleteTable() {
            SelectAthleteLogic select = new SelectAthleteLogic(ref _conn);
            select.Execute();
            return select.List;
        }

        public static void PrintAthletes(IEnumerable<AthleteDto> list) {
            Console.WriteLine(string.Join("\n", list));
        }

        //public static void PrintAthletes(IEnumerable<Athlete> list) {
        //    Console.WriteLine(string.Join("\n", list));
        //}
    }
}
