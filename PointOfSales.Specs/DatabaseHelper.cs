using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Specs
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = "server=(localdb)\\v11.0;database=PoS;Integrated Security=SSPI";

        public static void CreateProductsTable()
        {
            string sql = @"
IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('Products'))
BEGIN;
    DROP TABLE Products;
END;

CREATE TABLE Products (
    ProductID INTEGER NOT NULL IDENTITY(1, 1),
    Name VARCHAR(255) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Description VARCHAR(MAX) NOT NULL,
    PictureURL VARCHAR(255) NULL,
    EntryDate DATETIME NOT NULL,
    PRIMARY KEY (ProductID)
);
";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void SeedProducts()
        {
            string sql = @"
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('iPhone 5',500,'Cool smartphone','QNS18KHI0IN','10/01/2013');
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('Lumia 1020',700,'Smartphone with best camera','KRX44RFV9MV','04/20/2015');
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('20-pin Adapter',50,'Adapter for charging iPhone','MUZ33EWM5BG','01/19/2014');
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('Motorola Defy',800,'Unbreakable smartphone','FNN66UJW9GE','12/26/2013');
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('Case for iPhone',100,'Boostcase Hybrid Power Case for iPhone','UDL72FJM2NM','12/04/2013');
";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
