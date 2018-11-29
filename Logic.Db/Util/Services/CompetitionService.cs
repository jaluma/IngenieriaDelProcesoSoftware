using System.Collections.Generic;
using System.Data;
using System.Linq;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.ActionObjects.CategoriesLogic;
using Logic.Db.ActionObjects.CompetitionLogic;
using Logic.Db.ActionObjects.TimesLogic;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services
{
    public class CompetitionService : ServiceAdapter
    {
        public DataTable SelectCompetitionFinish() {
            var selectCompetition = new SelectCompetitionTimeLogic(ref _conn);
            selectCompetition.Execute();
            return selectCompetition.Table;
        }

        public DataTable ListOpenCompetitions() {
            var listOpenCompetition = new ListOpenCompetitionLogic(ref _conn);
            listOpenCompetition.Execute();
            return listOpenCompetition.Table;
        }

        public DataTable ListPreInscriptionCompetitions() {
            var listPreCompetition = new ListPreInscriptionCompetitionLogic(ref _conn);
            listPreCompetition.Execute();
            return listPreCompetition.Table;
        }

        public DataTable ListNotRealizedCompetitions() {
            var listCompetition = new ListNotRealizedCompetitionLogic(ref _conn);
            listCompetition.Execute();
            return listCompetition.Table;
        }

        public CompetitionDto SelectCompetitionDtoFromName(CompetitionDto competition) {
            var select = new SelectCompetitionLogic(ref _conn);
            select.Execute();
            return select.List.First(s => s.Name.Equals(competition.Name));
        }

        public DataTable ListCompetitionsToInscribe(AthleteDto athlete) {
            var competitions = new CompetitionsToInscribeLogic(ref _conn, athlete);
            competitions.Execute();
            return competitions.Table;
        }

        public List<CompetitionDto> ListCompetitionsToInscribeObject(AthleteDto athlete) {
            var competitions = new CompetitionsToInscribeLogic(ref _conn, athlete);
            competitions.Execute();
            return competitions.Competitions;
        }

        public DataTable ListCompetitionsToPreinscribe(AthleteDto athlete) {
            var competitions = new CompetitionsToPreinscribeLogic(ref _conn, athlete);
            competitions.Execute();
            return competitions.Table;
        }

        public List<CompetitionDto> ListCompetitionsToPreinscribeObject(AthleteDto athlete) {
            var competitions = new CompetitionsToPreinscribeLogic(ref _conn, athlete);
            competitions.Execute();
            return competitions.Competitions;
        }

        public DataTable SelectAllCompetitions() {
            var competitions = new SelectAllCompetition(ref _conn);
            competitions.Execute();
            return competitions.Table;
        }

        public DataTable SelectRaffleCompetitions() {
            var competitions = new ListCompetitionsForRaffle(ref _conn);
            competitions.Execute();
            return competitions.Table;
        }

        public CompetitionDto SearchCompetitionById(CompetitionDto competition) {
            var searchCompetition = new SearchCompetitionByIdLogic(ref _conn, competition);
            searchCompetition.Execute();
            return searchCompetition.Competition;
        }

        public DataTable SelectAllCompetitionsInscripted(string dni) {
            var competitions = new SelectAllCompetitionsInscripted(ref _conn, dni);
            competitions.Execute();
            return competitions.Table;
        }

        public void AddCompetition(CompetitionDto competition) {
            var add = new AddCompetitionLogic(ref _conn, competition);
            add.Execute();
        }

        public void AddCategory(CategoryDto category) {
            var add = new AddCategoryLogic(ref _conn, category);
            add.Execute();
        }

        public void AddInscriptionDate(InscriptionDatesDto plazo, CompetitionDto competition) {
            var add = new AddInscriptionDates(ref _conn, plazo, competition);
            add.Execute();
        }

        public byte[] GetRules(CompetitionDto competition) {
            var rules = new GetRulesCompetitionLogic(ref _conn, competition);
            return rules.Execute();
        }

        public CategoryDto SelectCompetitionByAthleteAndCompetition(CompetitionDto competition, AthleteDto athlete) {
            var category = new SelectCategoryByAthleteAndCompetition(ref _conn, competition, athlete);
            category.Execute();
            return category.Category;
        }

        public IEnumerable<AbsoluteCategory> SelectAllCategories() {
            var cat = new SelectCategoriesPredefinied(ref _conn);
            cat.Execute();
            return cat.List;
        }

        public CategoryDto GetCategory(CategoryDto c) {
            var search = new SearchCategoryChild(ref _conn, c);
            search.Execute();
            return search.Child;
        }

        public int GetIdCompetition(CompetitionDto c) {
            var search = new GetIdCompetition(ref _conn, c);
            search.Execute();
            return search.id;
        }

        public int GetIdAbsolute(AbsoluteCategory c) {
            var search = new GetAbsoluteCategory(ref _conn, c);
            search.Execute();
            return search.id;
        }

        public void AddAbsoluteCategory(AbsoluteCategory category) {
            var add = new AddAbsoluteCategory(ref _conn, category);
            add.Execute();
        }


        public int SelectNumberRaffle(long competitionId) {
            var select = new SelectAtheletesRaffle(ref _conn, competitionId);
            select.Execute();
            return select.numeroPlazas;
        }

        public void ChangeToClosed(long id) {
            var change = new ChangeToClosed(ref _conn, id);
            change.Execute();
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
            var categories = new SelectAllCategoriesByCompetitionId(ref _conn, competition);
            categories.Execute();
            return categories.Categories;
        }

        public IEnumerable<CategoryDto> SelectCategoryByAbsoluteCategories(int[] id) {
            var categories = new SelectCategoryByAbsolutCategory(ref _conn, id);
            categories.Execute();
            return categories.Categories;
        }

        public double SelectCompetitionPrice(string dni, long id) {
            var select = new SelectCompetitionPrice(ref _conn, dni, id);
            select.Execute();
            return select.Price;
        }

        public DataTable ListCompetitionsToInscribeClubs(int count) {
            var competitions = new CompetitionToInscribeClubs(ref _conn, count);
            competitions.Execute();
            return competitions.Table;
        }
    }
}
