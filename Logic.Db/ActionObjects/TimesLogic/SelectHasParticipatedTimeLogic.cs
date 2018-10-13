using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.TimesLogic {

    public class SelectHasParticipatedTimeLogic : IActionObject {
        private readonly DBConnection _conn;
        public readonly DataTable Table;
        private readonly CompetitionDto _competition;

        private bool _maleFilter;
        private bool _femaleFilter;

        public SelectHasParticipatedTimeLogic(ref DBConnection conn, CompetitionDto competition, bool? male, bool? female) {
            _conn = conn;
            Table = new DataTable();
            _competition = competition;
            _maleFilter = (bool) male;
            _femaleFilter = (bool) female;
        }
        public void Execute() {
            try {
                using (var connection = _conn.DbConnection) {
                    using (SQLiteCommand command = new SQLiteCommand(SqlCommandGenerator(), connection)) {
                        command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);

                        SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                        da.Fill(Table);
                    }
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }

        public void MaleFilterSwitch() {
            _maleFilter = !_maleFilter;
        }

        public void FemaleFilterSwitch() {
            _femaleFilter = !_femaleFilter;
        }

        private string SqlCommandGenerator() {
            string sql = Logic.Db.Properties.Resources.SQL_SELECT_ATHLETES_TIMES;
            if (_maleFilter || _femaleFilter)
                sql += " AND ";
            if (_maleFilter)
                sql += "ATHLETE_GENDER='M'";
            if (_maleFilter && _femaleFilter)
                sql += " OR ";
            if (_femaleFilter)
                sql += "ATHLETE_GENDER='F'";
            sql += " order by FINISH_TIME - INITIAL_TIME";

            return sql;
        }
    }
}
