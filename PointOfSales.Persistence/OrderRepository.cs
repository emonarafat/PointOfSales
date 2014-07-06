using NLog;
using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace PointOfSales.Persistence
{
    public class OrderRepository : Repository, IOrderRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Order GetById(int id)
        {
            Logger.Debug("Getting order {0}", id);
            var sql = "SELECT * FROM Orders WHERE OrderID = @id";

            using (var conn = GetConnection())
                return conn.Query<Order>(sql, new { id }).FirstOrDefault();
        }
    }
}
