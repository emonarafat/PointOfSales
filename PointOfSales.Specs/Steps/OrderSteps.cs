using FizzWare.NBuilder;
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
        private OrdersApi ordersApi;
        private OrderLinesApi orderLinesApi;
        private SalesCombinationsApi salesCombinationsApi;
        private List<Product> products;
        private int orderId;

        public OrderSteps(OrdersApi ordersApi, OrderLinesApi orderLinesApi, SalesCombinationsApi salesCombinationsApi)
        {
            this.ordersApi = ordersApi;
            this.orderLinesApi = orderLinesApi;
            this.salesCombinationsApi = salesCombinationsApi;
        }

        [Given(@"I have an empty order")]
        public void GivenIHaveAnEmptyOrder()
        {
            orderId = DatabaseHelper.Save(Builder<Order>.CreateNew().Build());
            products = DatabaseHelper.GetProducts().ToList();
        }

        [When(@"I add '(.*)' to this order")]
        public void WhenIAddProductToOrder(string productName)
        {
            int productId = GetProductId(productName);
            orderLinesApi.AddOrderLine(orderId, productId, quantity: 1);
        }

        [Then(@"order should have following lines")]
        public void ThenOrderShouldHaveFollowingLines(Table table)
        {
            var expectedLines = table.Rows.Select(r => new {
                ProductName = r["ProductName"],
                Quantity = Int32.Parse(r["Quantity"])
            }).OrderBy(x => x.ProductName).ThenBy(x => x.Quantity);

            var lines = orderLinesApi.GetOrderLines(orderId);
            var actualLines = lines.Select(l => new {
                ProductName = products.First(p => p.ProductId == l.ProductId).Name,
                l.Quantity
            }).OrderBy(x => x.ProductName).ThenBy(x => x.Quantity);

            Assert.Equal(expectedLines, actualLines);
        }

        [When(@"I add following sales combination to this order")]
        public void WhenIAddFollowingSalesCombinationToThisOrder(Table table)
        {
            var row = table.Rows[0];
            var mainProductId = GetProductId(row["MainProduct"]);
            var subProductId = GetProductId(row["SubProduct"]);
            var sales = DatabaseHelper.GetSalesCombinations();
            var salesCombinationId = sales
                .First(s => s.MainProductId == mainProductId && s.SubProductId == subProductId)
                .SalesCombinationId;

            salesCombinationsApi.Post(orderId, salesCombinationId);
        }

        [Then(@"total price should be (.*)")]
        public void ThenTotalPriceShouldBe(int totalPrice)
        {
            var order = ordersApi.Get(orderId);
            Assert.Equal(totalPrice, order.TotalPrice);
        }

        private int GetProductId(string productName)
        {
            return products.First(p => p.Name == productName).ProductId;
        }
    }
}
