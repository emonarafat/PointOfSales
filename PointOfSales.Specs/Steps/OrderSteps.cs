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
    public class OrderSteps
    {
        [Given(@"I have an empty order")]
        public void GivenIHaveAnEmptyOrder()
        {
            DatabaseHelper.CreateProductsTable();
            DatabaseHelper.SeedProducts();
            DatabaseHelper.CreateOrdersTable();
            DatabaseHelper.SeedOrders();
            DatabaseHelper.CreateOrderLinesTable();
            DatabaseHelper.CreateSalesCombinationsTable();
            DatabaseHelper.SeedSalesCombinations();
        }

        [When(@"I add product to order")]
        public void WhenIAddProductToOrder()
        {
            WebApiHelper.Post("api/orderlines", new OrderLine { OrderId = 1, ProductId = 1, Quantity = 1 });
        }

        [Then(@"order should have order line with product")]
        public void ThenOrderShouldHaveOrderLineWithProduct()
        {
            var lines = WebApiHelper.Get<List<OrderLine>>("api/orderlines/order/1");
            Assert.Equal(1, lines.Count);
        }

        [When(@"I add sales combination to order")]
        public void WhenIAddSalesCombinationToOrder()
        {
            WebApiHelper.Post("api/orders/1/sales/2", new object());
        }

        [Then(@"order should contain both products")]
        public void ThenOrderShouldContainBothProducts()
        {
            var lines = WebApiHelper.Get<List<OrderLine>>("api/orderlines/order/1");
            Assert.Equal(2, lines.Count);
        }

        [Then(@"total price should have discount")]
        public void ThenTotalPriceShouldHaveDiscount()
        {
            var order = WebApiHelper.Get<Order>("api/orders/1");
            Assert.Equal(580, order.TotalPrice);
        }

        [When(@"I add two products to order")]
        public void WhenIAddTwoProductsToOrder()
        {
            WebApiHelper.Post("api/orderlines", new OrderLine { OrderId = 1, ProductId = 1, Quantity = 1 });
            WebApiHelper.Post("api/orderlines", new OrderLine { OrderId = 1, ProductId = 2, Quantity = 1 });
        }

        [Then(@"order line quantity should be (.*)")]
        public void ThenOrderLineQuantityShouldBe(int quantity)
        {
            var line = WebApiHelper.Get<List<OrderLine>>("api/orderlines/order/1").Single();
            Assert.Equal(quantity, line.Quantity);
        }

        [When(@"I add sub product to order")]
        public void WhenIAddSubProductToOrder()
        {
            WebApiHelper.Post("api/orderlines", new OrderLine { OrderId = 1, ProductId = 3, Quantity = 1 });
        }

        [Then(@"items quantity should be increased")]
        public void ThenItemsQuantityShouldBeIncreased()
        {
            var line = WebApiHelper.Get<List<OrderLine>>("api/orderlines/order/1").ToArray();
            Assert.Equal(2, line[0].Quantity);
            Assert.Equal(2, line[1].Quantity);
        }
    }
}
