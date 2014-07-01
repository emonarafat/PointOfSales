using Microsoft.Owin.Hosting;
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
            using(SqlConnection conn = new SqlConnection(connectionString))
            using(SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
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
    }
}
