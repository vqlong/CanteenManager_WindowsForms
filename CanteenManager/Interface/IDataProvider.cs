using System.Data;

namespace CanteenManager.Interface
{
    public interface IDataProvider
    {
        int ExecuteNonQuery(string query, object[] parameter = null);
        DataTable ExecuteQuery(string query, object[] parameter = null);
        object ExecuteScalar(string query, object[] parameter = null);
        bool TestConnection(string connectionStr);
    }
}