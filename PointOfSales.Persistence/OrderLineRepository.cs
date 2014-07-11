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
    public class OrderLineRepository : Repository, IOrderLineRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<OrderLine> GetByOrder(int orderId)
        {
            Logger.Debug("Getting order lines of order {0}", orderId);
            var sql = "SELECT * FROM OrderLines WHERE OrderID = @orderId";

            using (var conn = GetConnection())
                return conn.Query<OrderLine>(sql, new { orderId });              
        }

        public void Add(OrderLine line)
        {
            Logger.Debug("Adding order line to order {0}", line.OrderId);
            var sql = @"INSERT INTO OrderLines (OrderID, ProductID, Price, Quantity) 
                            VALUES (@orderId, @productId, @price, @quantity)";

            using (var conn = GetConnection())            
                conn.Execute(sql, line);                        
        }

        public void Update(OrderLine line)
        {
            Logger.Debug("Updating order line {0}", line.OrderId);
            var sql = @"UPDATE OrderLines SET Quantity = @quantity WHERE OrderLineID = @id";

            using (var conn = GetConnection())            
                conn.Execute(sql, new { id = line.OrderLineId, quantity = line.Quantity });
        }
    }
}
