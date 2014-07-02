using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NLog;

namespace PointOfSales.Persistence
{
    public class SalesCombinationRepository : Repository, ISalesCombinationRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<SalesCombination> GetByProductId(int productId)
        {
            Logger.Debug("Getting sales combinations for product {0}", productId);
            var sql = @"SELECT * FROM SalesCombinations 
                        WHERE MainProductID = @productId OR SubProductID = @productId";

            using (var conn = GetConnection())
                return conn.Query<SalesCombination>(sql, new { productId });
        }
    }
}
