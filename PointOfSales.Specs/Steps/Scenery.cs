using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace PointOfSales.Specs.Steps
{
    [Binding]
    public class Scenery
    {
        [BeforeScenario("products", "orders", "sales")]
        public void CreateProductsTable()
        {
            DatabaseHelper.CreateProductsTable();
        }

        [BeforeScenario("sales", "orders")]
        public void CreateSalesCombinationsTable()
        {
            DatabaseHelper.CreateSalesCombinationsTable();
        }

        [BeforeScenario("orders")]
        public void CreateOrdersTable()
        {
            DatabaseHelper.CreateOrdersTable();
        }

        [BeforeScenario("orders")]
        public void CreateOrderLinesTable()
        {
            DatabaseHelper.CreateOrderLinesTable();
        }

        [BeforeScenario("customers")]
        public void CreateCustomersTable()
        {
            DatabaseHelper.CreateCustomersTable();
        }

        [AfterScenario("products", "orders", "sales")]
        public void DropProductsTable()
        {
            DatabaseHelper.DropTable("Products");
        }

        [AfterScenario("sales", "orders")]
        public void DropSalesCombinationsTable()
        {
            DatabaseHelper.DropTable("SalesCombinations");
        }

        [AfterScenario("orders")]
        public void DropOrdersTable()
        {
            DatabaseHelper.DropTable("Orders");
        }

        [AfterScenario("orders")]
        public void DropOrderLinesTable()
        {
            DatabaseHelper.DropTable("OrderLines");
        }

        [AfterScenario("customers")]
        public void DropCustomersTable()
        {
            DatabaseHelper.DropTable("Customers");
        }
    }
}
