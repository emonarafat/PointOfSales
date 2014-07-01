using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PointOfSales.Persistence
{
    public class SalesCombinationRepository : ISalesCombinationRepository
    {
        private static readonly string connectionString = "server=(localdb)\\v11.0;database=PoS;Integrated Security=SSPI";

        public IEnumerable<SalesCombination> GetByProductId(int productId)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = @"SELECT * FROM SalesCombinations WHERE MainProductID = @productId";
                return conn.Query<SalesCombination>(sql, new { productId });
            }  
        }
    }
}
