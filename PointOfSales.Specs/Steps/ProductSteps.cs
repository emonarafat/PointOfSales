using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PointOfSales.Domain.Model;
using PointOfSales.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace PointOfSales.Specs.Steps
{
    [Binding]
    public class ProductSteps
    {
        private HttpResponseMessage response;

        private static readonly string connectionString = "server=(localdb)\\v11.0;database=PoS;Integrated Security=SSPI";

        [Given(@"I have no products")]
        public void GivenIHaveNoProducts()
        {
            CreateProductsTable();
        }

        [When(@"I am trying to see all available products")]
        public void WhenIAmTryingToSeeAllAvailableProducts()
        {
            string baseAddress = "http://localhost:9000/";
            var type = typeof(ProductsController);

            using(WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                response = client.GetAsync(baseAddress + "api/products").Result;                
            }
        }

        [Then(@"I do not see any products")]
        public void ThenIDoNotSeeAnyProducts()
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("[]", response.Content.ReadAsStringAsync().Result);
        }

        [Given(@"I have some products")]
        public void GivenIHaveSomeProducts()
        {
            CreateProductsTable();

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

        [Then(@"I see all products")]
        public void ThenISeeAllProducts()
        {
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string json = response.Content.ReadAsStringAsync().Result;            
            var products = JsonConvert.DeserializeObject<List<Product>>(json);
            Assert.Equal(5, products.Count);
        }

        private void CreateProductsTable()
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
    }
}
