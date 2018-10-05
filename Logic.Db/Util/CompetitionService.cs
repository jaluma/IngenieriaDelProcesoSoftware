using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.ActionObjects.CompetitionLogic;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.Util {
    public class CompetitionService {

        private DBConnection _conn;

        public CompetitionService(ref DBConnection conn) {
            _conn = conn;
        }

        //public void InsertAthletesTable(AthleteDto athleteP) {
        //    AddAthleteLogic add = new AddAthleteLogic(ref _conn, athleteP);
        //    add.Execute();
        //}

        //public List<CompetitionDto> SelectAthleteTable() {
        //    SelectCompetitionLogic select = new SelectCompetitionLogic(ref _conn, c => c.Km > 50);
        //    select.Execute();
        //    return select.List;
        //}

        //public void DeleteAthleteTable(AthleteDto athleteP) {
        //    DeleteAthleteLogic delete = new DeleteAthleteLogic(ref _conn, athleteP);
        //    delete.Execute();
        //}

        //public static void PrintAthletes(IEnumerable<AthleteDto> list) {
        //    Console.WriteLine(string.Join("\n", list));
        //}

        //public static void PrintAthletes(IEnumerable<Athlete> list) {
        //    Console.WriteLine(string.Join("\n", list));
        //}
    }
}
