using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Persistence
{
    public class Repository
    {
        private static readonly string connectionString;

        static Repository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["pos"].ConnectionString;
        }

        protected IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
