﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.AthleteLogic.Enroll {
    public class IsDorsalsByCompetition : IActionObject{

        private readonly DBConnection _conn;
        private readonly CompetitionDto _competition;
        public bool IsDorsals;

        public  IsDorsalsByCompetition(ref DBConnection conn, CompetitionDto competitionP) {
            _conn = conn;
            _competition = competitionP;
            IsDorsals = false;
        }

        public void Execute() {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Properties.Resources.SQL_SELECT_COUNT_DORSALS_BY_COMPETITION, _conn.DbConnection)) {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition.ID);
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        if (!reader.HasRows) {
                            throw new SQLiteException();
                        }

                        reader.Read();
                        IsDorsals = reader.GetInt32(0) != 0;
                    }
                }

            } catch (SQLiteException) {
                _conn.DbConnection?.Close();
                throw;
            }
            
        }
    }
}
