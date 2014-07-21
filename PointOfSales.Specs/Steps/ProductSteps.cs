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
        private List<Product> products;

        [Given(@"I have no products")]
        public void GivenIHaveNoProducts()
        {
            DatabaseHelper.CreateProductsTable();
        }

        [When(@"I am trying to see all available products")]
        public void WhenIAmTryingToSeeAllAvailableProducts()
        {
            products = WebApiHelper.Get<List<Product>>("api/products");
        }

        [Then(@"I do not see any products")]
        public void ThenIDoNotSeeAnyProducts()
        {
            Assert.Equal(0, products.Count);
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
            Assert.Equal(5, products.Count);
        }

        [When(@"I search products by name")]
        public void WhenISearchProductsByName()
        {
            // TODO: Use step parameter
            products = WebApiHelper.Get<List<Product>>("api/products?search=lumia");
        }

        [Then(@"I see products with names containing search string")]
        public void ThenISeeProductsWithNamesContainingSearchString()
        {           
            Assert.Equal(1, products.Count);
            Assert.True(products.All(p => p.Name.IndexOf("lumia", StringComparison.InvariantCultureIgnoreCase) >= 0));
        }

        [When(@"I search products by description")]
        public void WhenISearchProductsByDescription()
        {
            products = WebApiHelper.Get<List<Product>>("api/products?search=smartphone");
        }

        [Then(@"I see products with description containing search string")]
        public void ThenISeeProductsWithDescriptionContainingSearchString()
        {
            Assert.Equal(3, products.Count);
            Assert.True(products.All(p => p.Description.IndexOf("smartphone", StringComparison.InvariantCultureIgnoreCase) >= 0));
        }

        [When(@"I search products by name or description")]
        public void WhenISearchProductsByNameOrDescription()
        {
            products = WebApiHelper.Get<List<Product>>("api/products?search=iphone");
        }

        [Then(@"I see products with either name or description containing search string")]
        public void ThenISeeProductsWithEitherNameOrDescriptionContainingSearchString()
        {
            Assert.Equal(3, products.Count);
        }
    }
}
