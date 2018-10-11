using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace Logic.Db.Connection {
    public class DBConnection {

        private static readonly string SourceFile = System.IO.Path.Combine(Logic.Db.Properties.Resources.DbPath, Logic.Db.Properties.Resources.DbFileName);
        private static readonly string DestFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) ?? throw new InvalidOperationException(), Logic.Db.Properties.Resources.DbFileName);

        private SQLiteConnection _dbConnection;

        public SQLiteConnection DbConnection {
            get {
                if (_dbConnection == null) {
                    DBConnection.Inicialize();

                    _dbConnection = new SQLiteConnection($"Data Source={Logic.Db.Properties.Resources.DbFileName};Version=3;Pooling=True;Max Pool Size=1500;");
                    //_dbConnection.Open();
                }

                return _dbConnection.OpenAndReturn();
            }
        }

        private static void Inicialize() {
            if (!DbIsCreated()) {
                // copy database.db resource to execution path
                try {
                    File.WriteAllBytes(DestFile.Substring(8), Logic.Db.Properties.Resources.Database);
                } catch (IOException) { }
            }
        }

        private static bool DbIsCreated() => File.Exists(DestFile);
    }
}
