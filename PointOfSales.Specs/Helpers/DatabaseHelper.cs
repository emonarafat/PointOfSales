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

        internal static void CreateProductsTable()
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
            Execute(sql);
        }

        internal static void SeedProducts()
        {
            string sql = @"
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('iPhone 5',500,'Cool smartphone','QNS18KHI0IN','10/01/2013');
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('Lumia 1020',700,'Smartphone with best camera','KRX44RFV9MV','04/20/2015');
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('20-pin Adapter',50,'Adapter for charging iPhone','MUZ33EWM5BG','01/19/2014');
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('Motorola Defy',800,'Unbreakable smartphone','FNN66UJW9GE','12/26/2013');
INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) VALUES('Case for iPhone',100,'Boostcase Hybrid Power Case for iPhone','UDL72FJM2NM','12/04/2013');
";
            Execute(sql);
        }
        internal static void CreateSalesCombinationsTable()
        {
            string sql = @"
IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('SalesCombinations'))
BEGIN;
    DROP TABLE SalesCombinations;
END;

CREATE TABLE SalesCombinations (
    SalesCombinationID INTEGER NOT NULL IDENTITY(1, 1),    
    MainProductID INTEGER NOT NULL,
    SubProductID INTEGER NOT NULL,
    Discount DECIMAL(18,2) NOT NULL,
    PRIMARY KEY (SalesCombinationID)
);
";
            Execute(sql);           
        }

        internal static void SeedSalesCombinations()
        {
            string sql = @"
INSERT INTO SalesCombinations(MainProductID,SubProductID,Discount) VALUES(1,3,5);
INSERT INTO SalesCombinations(MainProductID,SubProductID,Discount) VALUES(1,5,20);
";
            Execute(sql);
        }
        internal static void CreateOrdersTable()
        {
            string sql = @"
IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('Orders'))
BEGIN;
    DROP TABLE Orders;
END;

CREATE TABLE Orders (
    OrderID INTEGER NOT NULL IDENTITY(1, 1),    
    CustomerID INTEGER NOT NULL,
    EntryDate DATETIME NOT NULL
);
";
            Execute(sql);
        }

        internal static void CreateOrderLinesTable()
        {
            string sql = @"
IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('OrderLines'))
BEGIN;
    DROP TABLE OrderLines;
END;

CREATE TABLE OrderLines (
    OrderLineID INTEGER NOT NULL IDENTITY(1, 1),    
    OrderID INTEGER NOT NULL,
    ProductID INTEGER NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Quantity INTEGER NOT NULL
);
";
            Execute(sql);
        }

        internal static void SeedOrders()
        {
            string sql = @"
INSERT INTO Orders(CustomerID,EntryDate) VALUES(1,'03/15/14');
INSERT INTO Orders(CustomerID,EntryDate) VALUES(1,'05/24/14');
";
            Execute(sql);
        }

        private static void Execute(string sql)
        {
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
