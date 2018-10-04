using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.Util {
    public static class AthletesService {

        public static void InsertAthletesTable(ref DBConnection conn, AthleteDto athleteP) {
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_INSERT_ATHLETE, conn.DbConnection)) {
                    command.Parameters.AddWithValue("@DNI", athleteP.Dni);
                    command.Parameters.AddWithValue("@NAME", athleteP.Name);
                    command.Parameters.AddWithValue("@SURNAME", athleteP.Surname);
                    command.Parameters.AddWithValue("@BIRTH_DATE", athleteP.BirthDate);
                    command.Parameters.AddWithValue("@GENDER", new string((char)athleteP.Gender, 1));
                    command.ExecuteNonQuery();
                }
            } catch (SQLiteException) {
                conn.DbConnection?.Close();
                throw;
            }
        }

        public static List<AthleteDto> SelectAthleteTable(ref DBConnection conn) {
            List<AthleteDto> list = null;
            try {
                using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_SELECT_ATHLETE, conn.DbConnection)) {
                    using (SQLiteDataReader reader = command.ExecuteReader()) {
                        list = new List<AthleteDto>();
                        while (reader.Read()) {
                            AthleteDto athlete = new AthleteDto() {
                                Dni = reader.GetString(0),
                                Name = reader.GetString(1),
                                Surname = reader.GetString(2),
                                BirthDate = reader.GetDateTime(3)
                            };
                            Enum.TryParse<Gender>(reader.GetString(4), out athlete.Gender);
                        }
                    }
                }
            } catch (SQLiteException) {
                conn.DbConnection?.Close();
                throw;
            }

            return list;
        }

        public static void PrintAthletes(IEnumerable<AthleteDto> list) {
            Console.WriteLine(string.Join("\n", list));
        }

        //public static void PrintAthletes(IEnumerable<Athlete> list) {
        //    Console.WriteLine(string.Join("\n", list));
        //}
    }
}
