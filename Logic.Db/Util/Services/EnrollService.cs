using System;
using System.Collections.Generic;
using System.Data;
using Logic.Db.ActionObjects.AthleteLogic.Enroll;
using Logic.Db.ActionObjects.CompetitionLogic;
using Logic.Db.Dto;
using Logic.Db.Dto.Types;

namespace Logic.Db.Util.Services
{
    public class EnrollService : ServiceAdapter
    {
        private readonly CompetitionDto _competition;

        public EnrollService(CompetitionDto competition) {
            _competition = competition;
        }

        public DataTable SelectAthlete() {
            var select = new SelectAthleteEnrollLogic(ref _conn, _competition);
            select.Execute();
            return select.Table;
        }

        public DataTable SelectAthleteRegistered() {
            var select = new SelectAthleteEnrollLogic(ref _conn, _competition);
            select.Execute();
            return select.Table;
        }

        public void InsertAthleteRegistered() {
            var insert = new InsertAthletesHasRegisteredLogic(ref _conn);
            insert.Execute();
        }

        public void UpdateAthleteRegisteredDorsal(CompetitionDto competition) {
            var update = new UpdateAthletesRegisteredDorsal(ref _conn, competition);
            update.Execute();
        }

        public bool IsCategoryInCompetition(CompetitionDto competition, AthleteDto athlete) {
            var isCategory = new IsCategoryInCompetitionLogic(ref _conn, _competition, athlete);
            isCategory.Execute();
            return isCategory.IsCorrect;
        }

        public void InsertAthleteInCompetition(AthleteDto athlete, CompetitionDto competition, TypesStatus status) {
            var insertAthletesInCompetition =
                new InsertAthletesInCompetitionLogic(ref _conn, athlete, competition, status);
            insertAthletesInCompetition.Execute();
        }

        public string GetCategory(AthleteDto athlete, CompetitionDto competition) {
            var getCategory = new GetCategoryLogic(ref _conn, competition, athlete);
            getCategory.Execute();
            return getCategory.Category;
        }

        public bool IsDorsalsInCompetition(CompetitionDto competition) {
            var dorsals = new IsDorsalsByCompetition(ref _conn, competition);
            dorsals.Execute();
            return dorsals.IsDorsals;
        }

        public List<PaymentDto> SelectOutstandingAthletes() {
            var select = new SelectOutstandingAthletes(ref _conn);
            select.Execute();
            return select.Preregistered;
        }

        public void UpdateInscriptionStatus(string dni, long id, string status) {
            var update = new UpdateInscriptionStatus(ref _conn, dni, id, status);
            update.Execute();
        }

        public void EnrollCompetitionDates(long id, InscriptionDatesDto date) {
            var enroll = new EnrollDatesCompetition(ref _conn, id, date);
            enroll.Execute();
        }

        public void EnrollAbsoluteCompetition(long id, long date) {
            var enroll = new EnrollAbsoluteCompetition(ref _conn, id, date);
            enroll.Execute();
        }

        public void EnrollRefundsCompetition(long id, DateTime date, double refund) {
            var enroll = new EnrollRefundsCompetition(ref _conn, id, date, refund);
            enroll.Execute();
        }

        public IEnumerable<AthleteDto> SelectAthleteHasParticipated() {
            var athleteHasParticipated = new SelectAthleteHasParticipated(ref _conn, _competition);
            athleteHasParticipated.Execute();
            return athleteHasParticipated.List;
        }

        public DataTable NotCanceledInscriptions(string dni) {
            var select = new SelectNotCanceledInscriptions(ref _conn, dni);
            select.Execute();
            return select.Table;
        }

        public string SelectStatusEnroll(string dni) {
            var status = new SelectStatusEnroll(ref _conn, _competition, dni);
            status.Execute();
            return status.Return;
        }

        public void InsertHasRegisteredTimes(string dni, long initial, long finish) {
            var update = new UpdateHasRegisteredTimes(ref _conn, dni, _competition.ID, initial, finish);
            update.Execute();
        }

        public void UpdateRefund(string dni, long id, double refund) {
            var update = new UpdateEnrollRefund(ref _conn, dni, id, refund);
            update.Execute();
        }

        public bool IsAthleteInComp(CompetitionDto competition, AthleteDto athlete) {
            var isAthlete = new IsAthleteInCompetition(ref _conn, competition, athlete);
            isAthlete.Execute();
            return isAthlete.IsEnroll;
        }
    }
}
