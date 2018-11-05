using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.ActionObjects.AthleteLogic.Enroll;
using Logic.Db.Dto;

namespace Logic.Db.Util.Services {
    public class EnrollService : ServiceAdapter {

        private readonly CompetitionDto _competition;

        public EnrollService(CompetitionDto competition) {
            _competition = competition;
        }

        public DataTable SelectAthlete() {
            SelectAthleteEnrollLogic select = new SelectAthleteEnrollLogic(ref _conn, _competition);
            select.Execute();
            return select.Table;
        }

        public DataTable SelectAthleteRegistered() {
            SelectAthleteEnrollLogic select = new SelectAthleteEnrollLogic(ref _conn, _competition);
            select.Execute();
            return select.Table;
        }

        public void InsertAthleteRegistered() {
            InsertAthletesHasRegisteredLogic insert = new InsertAthletesHasRegisteredLogic(ref _conn);
            insert.Execute();
        }

        public void UpdateAthleteRegisteredDorsal(CompetitionDto competition) {
            UpdateAthletesRegisteredDorsal update = new UpdateAthletesRegisteredDorsal(ref _conn, competition);
            update.Execute();
        }

        public bool IsCategoryInCompetition(CompetitionDto competition, AthleteDto athlete) {
            IsCategoryInCompetitionLogic isCategory = new IsCategoryInCompetitionLogic(ref _conn, _competition, athlete);
            isCategory.Execute();
            return isCategory.IsCorrect;
        }

        public void InsertAthleteInCompetition(AthleteDto athlete, CompetitionDto competition)
        {
            InsertAthletesInCompetitionLogic insertAthletesInCompetition = new InsertAthletesInCompetitionLogic(ref _conn, athlete, competition);
            insertAthletesInCompetition.Execute();
        }

        public string GetCategory(AthleteDto athlete, CompetitionDto competition)
        {
            GetCategoryLogic getCategory = new GetCategoryLogic(ref _conn, competition, athlete);
            getCategory.Execute();
            return getCategory.Category;
        }

        public bool IsDorsalsInCompetition(CompetitionDto competition) {
            IsDorsalsByCompetition dorsals = new IsDorsalsByCompetition(ref _conn, competition);
            dorsals.Execute();
            return dorsals.IsDorsals;
        }

        public List<PaymentDto> SelectPreregisteredAthletes()
        {
            SelectPreregisteredAthletes select = new SelectPreregisteredAthletes(ref _conn);
            select.Execute();
            return select.Preregistered;
        }

        public void UpdateInscriptionStatus(string dni, long id, string status)
        {
            UpdateInscriptionStatus update = new UpdateInscriptionStatus(ref _conn, dni, id, status);
            update.Execute();
        }
    }
}
