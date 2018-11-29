using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using Logic.Db.Properties;

namespace Logic.Db.Connection
{
    public class DBConnection
    {
        private static readonly string SourceFile = Path.Combine(Resources.DbPath, Resources.DbFileName);

        private static readonly string DestFile =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) ??
                throw new InvalidOperationException(), Resources.DbFileName);

        private static SQLiteConnection _dbConnection;

        public DBConnection() {
            Inicialize();
        }

        public SQLiteConnection DbConnection {
            get {
                if (_dbConnection == null)
                    _dbConnection =
                        new SQLiteConnection(
                                $"Data Source={Resources.DbFileName};Version=3;Pooling=True;Max Pool Size=1500;")
                            .OpenAndReturn();

                return _dbConnection;
            }
        }

        private static void Inicialize() {
            if (!DbIsCreated()) File.WriteAllBytes(DestFile.Substring(8), Resources.Database);
        }

        private static bool DbIsCreated() {
            return File.Exists(DestFile.Substring(8));
        }
    }
}
