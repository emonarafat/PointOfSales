using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TechTalk.SpecFlow;

namespace PointOfSales.Specs
{
    public class DatabaseHelper
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

        internal static void CreateCustomersTable()
        {
            string sql = @"
IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('Customers'))
BEGIN;
    DROP TABLE Customers;
END;

CREATE TABLE Customers (
    CustomerID INTEGER NOT NULL IDENTITY(1, 1),
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    MiddleName VARCHAR(255) NULL,
    EmailAddress VARCHAR(255) NOT NULL,
    Street VARCHAR(255) NULL,
    HouseNumber VARCHAR(10) NULL,
    PostalCode VARCHAR(6) NULL,
    City VARCHAR(255) NULL,    
    EntryDate DATETIME NOT NULL
);
";
            Execute(sql);
        }

        internal static void DropTable(string tableName)
        {
            string sqlFormat = @"
IF EXISTS(SELECT 1 FROM sys.tables WHERE object_id = OBJECT_ID('{0}'))
BEGIN;
    DROP TABLE {0};
END;";
            Execute(String.Format(sqlFormat, tableName));
        }

        internal static void Save(IEnumerable<Product> products)
        {
            string sql = @"INSERT INTO Products(Name,Price,Description,PictureURL,EntryDate) 
                           VALUES(@Name,@Price,@Description,@PictureURL,@EntryDate)";

            using (SqlConnection conn = new SqlConnection(connectionString))
                conn.Execute(sql, products);
        }

        internal static IEnumerable<Product> GetProducts()
        {
            string sql = @"SELECT ProductID,Name,Price,Description,PictureURL,EntryDate FROM Products";
            using (SqlConnection conn = new SqlConnection(connectionString))
                return conn.Query<Product>(sql);
        }

        internal static void Save(IEnumerable<SalesCombination> sales)
        {
            string sql = @"INSERT INTO SalesCombinations(MainProductID,SubProductID,Discount) 
                           VALUES(@MainProductID,@SubProductID,@Discount)";

            using (SqlConnection conn = new SqlConnection(connectionString))
                conn.Execute(sql, sales);
        }

        internal static IEnumerable<SalesCombination> GetSalesCombinations()
        {
            string sql = @"SELECT SalesCombinationID,MainProductID,SubProductID,Discount FROM SalesCombinations";
            using (SqlConnection conn = new SqlConnection(connectionString))
                return conn.Query<SalesCombination>(sql);
        }

        internal static int Save(Customer customer)
        {
            string sql = @"INSERT INTO Customers(FirstName,LastName,MiddleName,EmailAddress,Street,HouseNumber,PostalCode,City,EntryDate) 
                           VALUES(@FirstName,@LastName,@MiddleName,@EmailAddress,@Street,@HouseNumber,@PostalCode,@City,@EntryDate);
                           SELECT CAST(SCOPE_IDENTITY() AS INT)";

            using (SqlConnection conn = new SqlConnection(connectionString))
                return conn.Query<int>(sql, customer).Single();
        }

        internal static int Save(Order order)
        {
            string sql = @"INSERT INTO Orders(CustomerID,EntryDate) VALUES (@CustomerID,@EntryDate);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection conn = new SqlConnection(connectionString))
                return conn.Query<int>(sql, order).Single();
        }

        internal static IEnumerable<Customer> GetCustomers()
        {
            string sql = @"SELECT CustomerID,FirstName,LastName,MiddleName,EmailAddress,Street,HouseNumber,PostalCode,City,EntryDate
                           FROM Customers";

            using (SqlConnection conn = new SqlConnection(connectionString))
                return conn.Query<Customer>(sql);
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