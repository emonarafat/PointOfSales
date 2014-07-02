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
    public class ProductRepository : IProductRepository
    {
        private static readonly string connectionString = "server=(localdb)\\v11.0;database=PoS;Integrated Security=SSPI";

        public IEnumerable<Product> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM Products";
                return conn.Query<Product>(sql);
            }            
        }

        public IEnumerable<Product> GetByNameOrDescription(string search)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var sql = @"SELECT * FROM Products 
                            WHERE Name LIKE @search OR Description LIKE @search";
                return conn.Query<Product>(sql, new { search = String.Format("%{0}%", search) });
            }  
        }

        public Product GetById(int productId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM Products WHERE ProductID = @productId";
                return conn.Query<Product>(sql, new { productId }).FirstOrDefault();
            }            
        }
    }
}