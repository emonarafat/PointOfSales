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
    public class SalesCombinationRepository : Repository, ISalesCombinationRepository
    {
        public IEnumerable<SalesCombination> GetByProductId(int productId)
        {
            var sql = @"SELECT * FROM SalesCombinations 
                        WHERE MainProductID = @productId OR SubProductID = @productId";

            using (var conn = GetConnection())
            {                
                return conn.Query<SalesCombination>(sql, new { productId });
            }  
        }
    }
}
