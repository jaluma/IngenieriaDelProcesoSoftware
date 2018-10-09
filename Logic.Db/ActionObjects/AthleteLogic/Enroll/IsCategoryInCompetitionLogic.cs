using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {
    public class IsCategoryInCompetitionLogic : IActionObject{

        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        private readonly AthleteDto _athlete;
        public bool IsCorrect;

        public  IsCategoryInCompetitionLogic(ref DBConnection conn, CompetitionDto competitionP, AthleteDto athlete) {
            _conn = conn;
            _competition = competitionP;
            _athlete = athlete;
            IsCorrect = false;
        }

        public void Execute() {
            string category;
            try {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_SELECT_CATEGORY_IN_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        if (!reader.HasRows) {
                            throw new SQLiteException();
                        }

                        reader.Read();
                        category = reader.GetString(0);
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_SELECT_COMPETITION_CATEGORY, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            if (category.Equals(reader.GetString(0))) {
                                IsCorrect = true;
                                return;
                            }
                        }
                    }
                }
            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
            
        }
    }
}
