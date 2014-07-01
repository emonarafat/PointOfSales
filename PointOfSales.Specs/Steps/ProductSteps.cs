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

        [Given(@"I have no products")]
        public void GivenIHaveNoProducts()
        {
            DatabaseHelper.CreateProductsTable();
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
            Assert.Equal("[]", response.Content.ReadAsStringAsync().Result);
        }

        [Given(@"I have some products")]
        public void GivenIHaveSomeProducts()
        {
            DatabaseHelper.CreateProductsTable();
            DatabaseHelper.SeedProducts();
        }

        [Then(@"I see all products")]
        public void ThenISeeAllProducts()
        {
            string json = response.Content.ReadAsStringAsync().Result;            
            var products = JsonConvert.DeserializeObject<List<Product>>(json);
            Assert.Equal(5, products.Count);
        }

        [When(@"I search products by name")]
        public void WhenISearchProductsByName()
        {
            string baseAddress = "http://localhost:9000/";
            var type = typeof(ProductsController);

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                response = client.GetAsync(baseAddress + "api/products/search/lumia").Result;
            }
        }

        [Then(@"I see products with names containing search string")]
        public void ThenISeeProductsWithNamesContainingSearchString()
        {
            string json = response.Content.ReadAsStringAsync().Result;
            var products = JsonConvert.DeserializeObject<List<Product>>(json);
            Assert.Equal(1, products.Count);
        }
    }
}
