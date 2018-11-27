using System;
using System.Data.SQLite;
using Logic.Db.Connection;
using Logic.Db.Dto;

namespace Logic.Db.ActionObjects.CompetitionLogic
{
    class ChangeToClosed : IActionObject
    {
        

            private readonly DBConnection _conn;
            private readonly long _competition;


            public ChangeToClosed(ref DBConnection conn, long id)
            {
                _competition = id;
                _conn = conn;

            }

            public void Execute()
            {
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand(Logic.Db.Properties.Resources.SQL_CHANGE_CLOSED_STATUS, _conn.DbConnection))
                    {

                    command.Parameters.AddWithValue("@COMPETITION_ID", _competition);
                    command.Parameters.AddWithValue("@STATUS", "CLOSED");
                       

                        command.ExecuteNonQuery();
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