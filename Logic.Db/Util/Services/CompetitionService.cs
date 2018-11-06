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

        public DataTable ListCompetitionsToInscribe(AthleteDto athlete)
        {
            CompetitionsToInscribeLogic competitions = new CompetitionsToInscribeLogic(ref _conn, athlete);
            competitions.Execute();
            return competitions.Table;
        }

        public DataTable SelectAllCompetitions()
        {
            SelectAllCompetition competitions = new SelectAllCompetition(ref _conn);
            competitions.Execute();
            return competitions.Table;
        }

        public CompetitionDto SearchCompetitionById(CompetitionDto competition)
        {
            SearchCompetitionByIdLogic searchCompetition = new SearchCompetitionByIdLogic(ref _conn, competition);
            searchCompetition.Execute();
            return searchCompetition.Competition;
        }

        public DataTable SelectAllCompetitionsInscripted(string dni)
        {
            SelectAllCompetitionsInscripted competitions = new SelectAllCompetitionsInscripted(ref _conn, dni);
            competitions.Execute();
            return competitions.Table;
        }

        public void AddCompetition(CompetitionDto competition) {
            AddCompetitionLogic add = new AddCompetitionLogic(ref _conn, competition);
            add.Execute();
        }

        public byte[] GetRules(CompetitionDto competition)
        {
            GetRulesCompetitionLogic rules = new GetRulesCompetitionLogic(ref _conn, competition);
            return rules.Execute();
        }

        public IEnumerable<AbsoluteCategory> SelectAllCategories()
        {
            SelectCategoriesPredefinied cat = new SelectCategoriesPredefinied(ref _conn);
            cat.Execute();
            return cat.List;
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
        public IEnumerable<AbsoluteCategory> SelectAllCategoriesByCompetitionId(CompetitionDto competition) {
            SelectAllCategoriesByCompetitionId categories = new SelectAllCategoriesByCompetitionId(ref _conn, competition);
            categories.Execute();
            return categories.Categories;
        }

        public IEnumerable<CategoryDto> SelectCategoryByAbsoluteCategories(int[] id) {
            SelectCategoryByAbsolutCategory categories = new SelectCategoryByAbsolutCategory(ref _conn, id);
            categories.Execute();
            return categories.Categories;
        }
    }
}
