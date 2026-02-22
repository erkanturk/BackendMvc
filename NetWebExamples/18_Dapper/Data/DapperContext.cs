using Microsoft.Data.SqlClient;
using System.Data;

namespace _18_Dapper.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        //IDbConnection interface i ile lambda kullanarak bir sql yapısını method olarak kullanmayı sağladık.
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
