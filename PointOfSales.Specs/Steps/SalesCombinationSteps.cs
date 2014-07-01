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

        [Given(@"I have product without sales combinaions")]
        public void GivenIHaveProductWithoutSalesCombinaions()
        {
            DatabaseHelper.CreateProductsTable();
            DatabaseHelper.SeedProducts();
            DatabaseHelper.CreateSalesCombinationsTable();
            DatabaseHelper.SeedSalesCombinations();
        }


        [When(@"I am trying to see available sales combinations")]
        public void WhenIAmTryingToSeeAvailableSalesCombinations()
        {
            result = WebApiHelper.GetJson("api/sales/search/2");
        }

        [Then(@"I do not see any available sales combinations")]
        public void ThenIDoNotSeeAnyAvailableSalesCombinations()
        {
            var sales = JsonConvert.DeserializeObject<List<SalesCombination>>(result);
            Assert.False(sales.Any());
        }
    }
}
