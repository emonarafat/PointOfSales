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
    public class ProductRepository : Repository, IProductRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<Product> GetAll()
        {
            Logger.Debug("Getting all products");
            var sql = "SELECT * FROM Products";

            using (var conn = GetConnection())
                return conn.Query<Product>(sql);
        }

        public IEnumerable<Product> GetByNameOrDescription(string search)
        {
            Logger.Debug("Searching for '{0}' products", search);
            var sql = @"SELECT * FROM Products 
                        WHERE Name LIKE @search OR Description LIKE @search";

            using (var conn = GetConnection())
                return conn.Query<Product>(sql, new { search = String.Format("%{0}%", search) });
        }

        public Product GetById(int productId)
        {
            Logger.Debug("Getting product by id = {0}", productId);
            var sql = "SELECT * FROM Products WHERE ProductID = @productId";

            using (var conn = GetConnection())
            {
                var product = conn.Query<Product>(sql, new { productId }).FirstOrDefault();
                if (product == null)
                    Logger.Warn("Product not found");

                return product;
            }
        }

        public Product Add(Product product)
        {
            var sql = @"INSERT INTO Products (Name, Price, Description, PictureURL, EntryDate)
                        OUTPUT INSERTED.ProductID, INSERTED.EntryDate
                        VALUES (@Name, @Price, @Description, @PictureURL, GETDATE())";

            using(var conn = GetConnection())
            {
                var result = conn.Query(sql, product).Single();
                product.ProductId = result.ProductID;
                product.EntryDate = result.EntryDate;
                return product;
            }
        }
    }
}