using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZintegrujPL.DAL.Database
{
    public class DbConnectionFactory: IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            // Utwórz nowe połączenie SQL.
            return new SqlConnection(_connectionString);
        }
    }
}
