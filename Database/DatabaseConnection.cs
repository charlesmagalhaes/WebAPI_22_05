using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Database
{
    public class DatabaseConnection : IDatabaseConnection<NpgsqlConnection>
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["PostgresConexao"].ConnectionString;

            public NpgsqlConnection GetConnection()
            {
                return new NpgsqlConnection(_connectionString);
            }
    }
    
}
