using System.Collections.Generic;
using System.Data;
using System.Linq;
using Logic.Db.ActionObjects.CompetitionLogic;
using Logic.Db.ActionObjects.TimesLogic;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services {
    public class CompetitionService : ServiceAdapter {

        public DataTable SelectCompetitionFinish() {
            SelectCompetitionTimeLogic selectCompetition = new SelectCompetitionTimeLogic(ref _conn);
            selectCompetition.Execute();
            return selectCompetition.Table;
        }

        public DataTable ListOpenCompetitions()
        {
            ListOpenCompetitionLogic listOpenCompetition = new ListOpenCompetitionLogic(ref _conn);
            listOpenCompetition.Execute();
            return listOpenCompetition.Table;
        }

        public DataTable ListNotRealizedCompetitions()
        {
            ListNotRealizedCompetitionLogic listCompetition = new ListNotRealizedCompetitionLogic(ref _conn);
            listCompetition.Execute();
            return listCompetition.Table;
        }

        public CompetitionDto SelectCompetitionDtoFromName(CompetitionDto competition) {
            SelectCompetitionLogic select = new SelectCompetitionLogic(ref _conn);
            select.Execute();
            return @select.List.First(s => s.Name.Equals(competition.Name));
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
