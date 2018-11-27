using System;
using System.Collections.Generic;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.Connection;
using Logic.Db.Dto;
using System.Data;
using Logic.Db.ActionObjects.AthleteLogic.Enroll;

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

        public void ChangeStatusAthlete(AthleteDto athleteP, long id)
        {
            ChangeStatusAthlete change = new ChangeStatusAthlete(ref _conn, athleteP, id);
            change.Execute();
        }

        public int CountAthleteByDni(string dni) {
            CountAthleteByDniLogic search = new CountAthleteByDniLogic(ref _conn, dni);
            search.Execute();
            return search.Contador;
        }

        public DataTable SelectAthleteByDni(string dni) {
            SelectAthleteByDniLogic select = new SelectAthleteByDniLogic(ref _conn, dni);
            select.Execute();
            return select.Table;
        }

        public AthleteDto SelectAthleteByDniObject(string dni) {
            SelectAthleteByDniLogicObject select = new SelectAthleteByDniLogicObject(ref _conn, dni);
            select.Execute();
            return select.Athlete;
        }

        public DataTable SelectParticipatedByDni(string dni) {
            SelectCompParticipatedByAhtlete select = new SelectCompParticipatedByAhtlete(ref _conn, dni);
            select.Execute();
            return select.Table;
        }

        public static void PrintAthletes(IEnumerable<AthleteDto> list) {
            Console.WriteLine(string.Join("\n", list));
        }

        public string SelectDniFromDorsal(int dorsal, long competitionId) {
            SelectDniFromDorsal select = new SelectDniFromDorsal(ref _conn, competitionId, dorsal);
            select.Execute();
            return select.Dni;
        }


        public List<AthleteDto> SelectAtheletesRaffle( long competitionId)
        {
            SelectAtheletesRaffle select = new SelectAtheletesRaffle(ref _conn, competitionId);
            select.Execute();
            return select.atletas;
        }




        
        //public static void PrintAthletes(IEnumerable<Athlete> list) {
        //    Console.WriteLine(string.Join("\n", list));
        //}
    }
}
