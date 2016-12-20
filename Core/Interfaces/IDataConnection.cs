using System.Data;

namespace Core.Interfaces
{
    public interface IDataConnection
    {
        string ConnectionString { get; }
        IDbConnection GetConnection();
    }
}