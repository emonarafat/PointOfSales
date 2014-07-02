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
    public class OrderLineRepository : Repository, IOrderLineRepository
    {
        public IEnumerable<OrderLine> GetByOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM OrderLines WHERE OrderID = @orderId";
                return conn.Query<OrderLine>(sql, new { orderId });
            }  
        }

        public void Add(OrderLine line)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var sql = @"INSERT INTO OrderLines (OrderID, ProductID, Price, Quantity) 
                            VALUES (@orderId, @productId, @price, @quantity)";
                conn.Execute(sql, line);
            }            
        }
    }
}
