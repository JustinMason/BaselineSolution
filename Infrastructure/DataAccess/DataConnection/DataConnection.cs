using System.Data;
using System.Data.SqlClient;
using Core.Interfaces;

namespace Infrastructure.DataAccess.DataConnection
{
    public class DataConnection : IDataConnection
    {
        public DataConnection(string connectionstring)
        {
            ConnectionString = connectionstring;
        }

        public string ConnectionString { get; private set; }


        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
