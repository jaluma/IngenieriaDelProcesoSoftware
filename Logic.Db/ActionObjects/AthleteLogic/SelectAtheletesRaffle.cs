﻿using Logic.Db.Connection;
using Logic.Db.Dto;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.ActionObjects.AthleteLogic
{
    class SelectAtheletesRaffle
    {
        private readonly DBConnection _conn;
        private readonly long _competitionID;
        public readonly List<string> dniS;


        public SelectAtheletesRaffle(ref DBConnection conn, long competitionID)
        {
            _conn = conn;
            _competitionID = competitionID;
            dniS = new List<string>();

        }

        public void Execute()
        {
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETE_PREINSCRIPTION, _conn.DbConnection))
                {
                    command.Parameters.AddWithValue("@COMPETITION_ID", _competitionID);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {                           
                            dniS.Add(reader.GetString(1));
                        }
                    }

                }
            }
            catch (SQLiteException)
            {
                _conn.DbConnection?.Close();
                throw;
            }
        }
    }
}