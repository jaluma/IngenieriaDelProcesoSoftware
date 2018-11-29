using Logic.Db.Connection;

namespace Logic.Db.Util
{
    public class ServiceAdapter
    {
        protected DBConnection _conn;

        public ServiceAdapter() {
            _conn = new DBConnection();
        }

        //public void Dispose() {
        //    _conn.DbConnection?.Close();
        //}
    }
}
