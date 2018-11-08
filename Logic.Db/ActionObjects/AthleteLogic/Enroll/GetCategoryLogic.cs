using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {
    public class GetCategoryLogic : IActionObject {
        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        private readonly AthleteDto _athlete;

        public string Category {
            get; private set;
        }


        public GetCategoryLogic(ref DBConnection conn, CompetitionDto competitionP, AthleteDto athlete) {
            _conn = conn;
            _competition = competitionP;
            _athlete = athlete;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_SELECT_CATEGORY_IN_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", _athlete.Dni);
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        if (!reader.HasRows) {
                            throw new SQLiteException();
                        }

                        reader.Read();
                        Category = reader.GetString(0);
                    }
                }


            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}
