using System;
using System.Data.SQLite;
using System.IO;

namespace Logic.Db.Connection {
    public class DBConnection {

        private static readonly string sourceFile = System.IO.Path.Combine(Logic.Db.Properties.Resources.DbPath, Logic.Db.Properties.Resources.DbFileName);
        private static readonly string destFile = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) ?? throw new InvalidOperationException(), Logic.Db.Properties.Resources.DbFileName);

        private SQLiteConnection _dbConnection;

        public SQLiteConnection DbConnection {
            get {
                if (_dbConnection == null) {
                    DBConnection.Inicialize();

                    _dbConnection = new SQLiteConnection($"Data Source={Logic.Db.Properties.Resources.DbPath};Version=3;New=True;Compress=True;");
                    _dbConnection.Open();
                }

                return _dbConnection;
            }
        }

        private static bool DbIsCreated() => File.Exists(destFile);

        private static void Inicialize() {
            if (! DbIsCreated()) {
                System.IO.File.Copy(sourceFile, destFile, true);
            }
        }
    }
}
