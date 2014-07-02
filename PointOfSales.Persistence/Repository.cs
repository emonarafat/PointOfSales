using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Persistence
{
    public class Repository
    {
        private static readonly string connectionString = "server=(localdb)\\v11.0;database=PoS;Integrated Security=SSPI";

        protected IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
