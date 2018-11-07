using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.ActionObjects.AthleteLogic;
using Logic.Db.ActionObjects.AthleteLogic.Enroll;
using Logic.Db.ActionObjects.CompetitionLogic;
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

        public void EnrollCompetitionDates(long id, InscriptionDatesDto date)
        {
            EnrollDatesCompetition enroll = new EnrollDatesCompetition(ref _conn, id, date);
            enroll.Execute();
        }

        public void EnrollAbsoluteCompetition(long id, long date)
        {
            EnrollAbsoluteCompetition enroll = new EnrollAbsoluteCompetition(ref _conn, id, date);
            enroll.Execute();
        }

        public void EnrollRefundsCompetition(long id, DateTime date, double refund)
        {
            EnrollRefundsCompetition enroll = new EnrollRefundsCompetition(ref _conn, id, date, refund);
            enroll.Execute();
        }

        public IEnumerable<AthleteDto> SelectAthleteHasParticipated() {
            SelectAthleteHasParticipated athleteHasParticipated = new SelectAthleteHasParticipated(ref _conn, _competition);
            athleteHasParticipated.Execute();
            return athleteHasParticipated.List;
        }

        public DataTable NotCanceledInscriptions(string dni)
        {
            SelectNotCanceledInscriptions select = new SelectNotCanceledInscriptions(ref _conn, dni);
            select.Execute();
            return select.Table;
        }

        public string SelectStatusEnroll(string dni)
        {
            SelectStatusEnroll status = new SelectStatusEnroll(ref _conn, _competition, dni);
            status.Execute();
            return status.Return;
        }

        public void InsertHasRegisteredTimes(string dni, long time)
        {
            UpdateHasRegisteredTimes update = new UpdateHasRegisteredTimes(ref _conn, dni, _competition.ID, time);
            update.Execute();
        }
        
    }
}
