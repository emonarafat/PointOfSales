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
        private SalesCombinationsApi salesCombinationsApi;
        private List<Product> products;
        private List<SalesCombination> sales;

        public SalesCombinationSteps(SalesCombinationsApi salesCombinationsApi)
        {
            this.salesCombinationsApi = salesCombinationsApi;
        }

        [Given(@"there are following sales combinations in shop")]
        public void GivenThereAreFollowingSalesCombinations(Table table)
        {
            products = DatabaseHelper.GetProducts().ToList();
            var productIds = products.ToDictionary(p => p.Name, p => p.ProductId);

            var sales = table.Rows.Select(r => new SalesCombination
            {
                MainProductId = productIds[r["MainProduct"]],
                SubProductId = productIds[r["SubProduct"]],
                Discount = r.Keys.Contains("Discount") ? Decimal.Parse(r["Discount"]) : 0
            });
            
            DatabaseHelper.Save(sales);
        }

        [When(@"I view available sales combinations of product '(.*)'")]
        public void WhenIViewAvailableSalesCombinationsOfProduct(string productName)
        {
            int productId = products.First(p => p.Name == productName).ProductId;
            sales = salesCombinationsApi.GetSalesByProduct(productId);
        }

        [Then(@"I do not see any sales combinations")]
        public void ThenIDoNotSeeAnySalesCombinations()
        {
            Assert.Equal(0, sales.Count);
        }

        [Then(@"I see following sales combinations")]
        public void ThenISeeFolowwingSalesCombinations(Table table)
        {
            var expectedSales = table.Rows.Select(r => new { 
                MainProduct = r["MainProduct"],
                SubProduct = r["SubProduct"]
            }).OrderBy(x => x.MainProduct).ThenBy(x => x.SubProduct);

            var productNames = products.ToDictionary(p => p.ProductId, p => p.Name);
            var actualSales = sales.Select(s => new {
                MainProduct = productNames[s.MainProductId],
                SubProduct = productNames[s.SubProductId]
            }).OrderBy(x => x.MainProduct).ThenBy(x => x.SubProduct);

            Assert.Equal(expectedSales, actualSales);
        }

        
        public void CreateSalesCombinationsStorage()
        {
            //DatabaseHelper.CreateSalesCombinationsTable();
        }

        
        public void AfterSalesScenario()
        {
            //DatabaseHelper.DropSalesCombinationsTable();
        }
    }
}
