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
        private List<SalesCombination> sales;

        public SalesCombinationSteps(SalesCombinationsApi salesCombinationsApi)
        {
            this.salesCombinationsApi = salesCombinationsApi;
        }

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
            sales = salesCombinationsApi.GetSalesByProduct(2);
        }

        [Then(@"I do not see any available sales combinations")]
        public void ThenIDoNotSeeAnyAvailableSalesCombinations()
        {            
            Assert.False(sales.Any());
        }

        [When(@"I am trying to see available sales combinations of product with sub-product sales")]
        public void WhenIAmTryingToSeeAvailableSalesCombinationsOfProductWithSub_ProductSales()
        {
            sales = salesCombinationsApi.GetSalesByProduct(1);
        }

        [Then(@"I see sub-products sales combinations")]
        public void ThenISeeSub_ProductsSalesCombinations()
        {            
            Assert.Equal(2, sales.Count);
        }

        [When(@"I am trying to see available sales combinations of product with main products sales")]
        public void WhenIAmTryingToSeeAvailableSalesCombinationsOfProductWithMainProductsSales()
        {
            sales = salesCombinationsApi.GetSalesByProduct(3);
        }

        [Then(@"I see main products sales combinations")]
        public void ThenISeeMainProductsSalesCombinations()
        {           
            Assert.Equal(1, sales.Count);
        }
    }
}
