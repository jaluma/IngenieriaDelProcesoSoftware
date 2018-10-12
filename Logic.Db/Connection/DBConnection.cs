using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace Logic.Db.Connection {
    public class DBConnection {

        private static readonly string SourceFile = System.IO.Path.Combine(Logic.Db.Properties.Resources.DbPath, Logic.Db.Properties.Resources.DbFileName);
        private static readonly string DestFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) ?? throw new InvalidOperationException(), Logic.Db.Properties.Resources.DbFileName);

        private static SQLiteConnection _dbConnection;
        
        public SQLiteConnection DbConnection {
            get {
                if (_dbConnection == null) {
                    _dbConnection = new SQLiteConnection($"Data Source={Logic.Db.Properties.Resources.DbFileName};Version=3;Pooling=True;Max Pool Size=1500;").OpenAndReturn();
                }

                return _dbConnection;
            }
        }

        public DBConnection() {
            DBConnection.Inicialize();
        }

        private static void Inicialize() {
            if (!DbIsCreated()) {
                // copy database.db resource to execution path
                File.WriteAllBytes(DestFile.Substring(8), Logic.Db.Properties.Resources.Database);
            }
        }

        private static bool DbIsCreated() => File.Exists(DestFile.Substring(8));
    }
}
