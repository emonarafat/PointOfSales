using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;
using FizzWare.NBuilder;

namespace PointOfSales.Specs.Steps
{
    [Binding]
    public class ProductSteps
    {
        private ProductsApi productsApi;
        private List<Product> products;

        public ProductSteps(ProductsApi productsApi)
        {
            this.productsApi = productsApi;
        }

        [Given(@"there are no products in shop")]
        public void GivenIHaveNoProducts()
        {
            // Do not add any products to datastore
        }

        [Given(@"there are following products in shop")]
        public void GivenThereAreFollowingProductsInShop(Table table)
        {
            var products = table.CreateSet<Product>(() => Builder<Product>.CreateNew().Build());
            DatabaseHelper.Save(products);
        }

        [Given(@"there are (\d+) products in shop")]
        public void GivenThereAreProductsInShop(int productsCount)
        {
            var products = Builder<Product>.CreateListOfSize(productsCount).Build();
            DatabaseHelper.Save(products);
        }

        [When(@"I view all available products")]
        public void WhenIViewAllAvailableProducts()
        {
            products = productsApi.GetProducts();
        }

        [When(@"I search products by '(.*)'")]
        public void WhenISearchProductsBy(string search)
        {
            products = productsApi.Get(search);
        }

        [When(@"I add following product")]
        public void WhenIAddFollowingProduct(Table table)
        {
            var product = table.CreateInstance<Product>();
            productsApi.Post(product);
        }

        [Then(@"I do not see any products")]
        public void ThenIDoNotSeeAnyProducts()
        {
            Assert.Equal(0, products.Count);
        }

        [Then(@"I see (\d+) products")]
        public void ThenISeeProducts(int productsCount)
        {
            Assert.Equal(productsCount, products.Count);
        }

        [Then(@"I see only these products: (.*)")]
        public void ThenISeeOnlyFollowingProducts(string productNames)
        {
            var names = productNames.Split(',').Select(n => n.Trim(' ', '\'')).OrderBy(n => n);
            Assert.Equal(names, products.Select(p => p.Name).OrderBy(n => n));
        }

        [Then(@"I see following products")]
        public void ThenISeeFollowingProducts(Table table)
        {
            var expectedProducts = table.CreateSet<Product>();
            var actualProducts = productsApi.GetProducts();

            Assert.Equal(expectedProducts.Count(), actualProducts.Count());

            foreach(var expectedProduct in expectedProducts)
            {
                var actualProduct = actualProducts.FirstOrDefault(p => p.Name == expectedProduct.Name);
                Assert.Equal(expectedProduct.Description, actualProduct.Description);
                Assert.Equal(expectedProduct.Price, actualProduct.Price);
            }
        }
    }
}
