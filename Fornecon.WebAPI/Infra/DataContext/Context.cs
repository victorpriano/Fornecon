using System.Data;
using System.Data.SqlClient;

namespace Fornecon.WebAPI.Repository.DataContext
{
    public class Context : IDisposable
    {
        public SqlConnection Connection { get; set; }
        IConfiguration _configuration;
        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
            Connection = new SqlConnection(_configuration.GetConnectionString("Connection"));
            Connection.Open();
        }
        public void Dispose()
        {
            if(Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
