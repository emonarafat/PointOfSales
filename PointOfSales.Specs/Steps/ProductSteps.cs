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
        private string response;

        [Given(@"I have no products")]
        public void GivenIHaveNoProducts()
        {
            DatabaseHelper.CreateProductsTable();
        }

        [When(@"I am trying to see all available products")]
        public void WhenIAmTryingToSeeAllAvailableProducts()
        {
            response = WebApiHelper.GetJson("api/products");
        }

        [Then(@"I do not see any products")]
        public void ThenIDoNotSeeAnyProducts()
        {            
            Assert.Equal("[]", response);
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
            var products = JsonConvert.DeserializeObject<List<Product>>(response);
            Assert.Equal(5, products.Count);
        }

        [When(@"I search products by name")]
        public void WhenISearchProductsByName()
        {
            // TODO: Use step parameter
            response = WebApiHelper.GetJson("api/products/search/lumia");
        }

        [Then(@"I see products with names containing search string")]
        public void ThenISeeProductsWithNamesContainingSearchString()
        {            
            var products = JsonConvert.DeserializeObject<List<Product>>(response);
            Assert.Equal(1, products.Count);
            Assert.True(products.All(p => p.Name.IndexOf("lumia", StringComparison.InvariantCultureIgnoreCase) >= 0));
        }

        [When(@"I search products by description")]
        public void WhenISearchProductsByDescription()
        {
            response = WebApiHelper.GetJson("api/products/search/smartphone");
        }

        [Then(@"I see products with description containing search string")]
        public void ThenISeeProductsWithDescriptionContainingSearchString()
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(response);
            Assert.Equal(3, products.Count);
            Assert.True(products.All(p => p.Description.IndexOf("smartphone", StringComparison.InvariantCultureIgnoreCase) >= 0));
        }

        [When(@"I search products by name or description")]
        public void WhenISearchProductsByNameOrDescription()
        {
            response = WebApiHelper.GetJson("api/products/search/iphone");
        }

        [Then(@"I see products with either name or description containing search string")]
        public void ThenISeeProductsWithEitherNameOrDescriptionContainingSearchString()
        {
            var products = JsonConvert.DeserializeObject<List<Product>>(response);
            Assert.Equal(3, products.Count);
        }
    }
}
