using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Connection;

namespace Logic.Db.Util {
    public class ServiceAdapter : IService{
        protected DBConnection _conn;

        public ServiceAdapter() {
            _conn = new DBConnection();
        }

        public void Dispose() {
            _conn.DbConnection.Close();
        }
    }
}
