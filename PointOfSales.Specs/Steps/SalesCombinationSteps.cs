using Newtonsoft.Json;
using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace PointOfSales.Specs.Steps
{
    [Binding]
    public class SalesCombinationSteps
    {
        private string result;

        [Given(@"I have products and sales combinaions")]
        public void GivenIHaveProductsAndSalesCombinaions()
        {
            DatabaseHelper.CreateProductsTable();
            DatabaseHelper.SeedProducts();
            DatabaseHelper.CreateSalesCombinationsTable();
            DatabaseHelper.SeedSalesCombinations();
        }

        [When(@"I am trying to see available sales combinations of product without sales")]
        public void WhenIAmTryingToSeeAvailableSalesCombinationsOfProductWithoutSales()
        {
            result = WebApiHelper.GetJson("api/sales/search/2");
        }

        [Then(@"I do not see any available sales combinations")]
        public void ThenIDoNotSeeAnyAvailableSalesCombinations()
        {
            var sales = JsonConvert.DeserializeObject<List<SalesCombination>>(result);
            Assert.False(sales.Any());
        }

        [When(@"I am trying to see available sales combinations of product with sub-product sales")]
        public void WhenIAmTryingToSeeAvailableSalesCombinationsOfProductWithSub_ProductSales()
        {
            result = WebApiHelper.GetJson("api/sales/search/1");
        }

        [Then(@"I see sub-products sales combinations")]
        public void ThenISeeSub_ProductsSalesCombinations()
        {
            var sales = JsonConvert.DeserializeObject<List<SalesCombination>>(result);
            Assert.Equal(2, sales.Count);
        }

        [When(@"I am trying to see available sales combinations of product with main products sales")]
        public void WhenIAmTryingToSeeAvailableSalesCombinationsOfProductWithMainProductsSales()
        {
            result = WebApiHelper.GetJson("api/sales/search/3");
        }

        [Then(@"I see main products sales combinations")]
        public void ThenISeeMainProductsSalesCombinations()
        {
            var sales = JsonConvert.DeserializeObject<List<SalesCombination>>(result);
            Assert.Equal(1, sales.Count);
        }
    }
}
