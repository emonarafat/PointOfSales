using Moq;
using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using PointOfSales.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PointOfSales.Tests
{
    public class OrderLinesControllerTests
    {
        private static readonly Random Random = new Random();

        [Fact]
        public void ShouldReturnOrderLinesOfOrder()
        {            
            var controller = CreateTestableOrderLinesController();
            int orderId = Random.Next();
            var expectedLines = Enumerable.Empty<OrderLine>();
            controller.OrderLineRepositoryMock.Setup(r => r.GetByOrder(orderId)).Returns(expectedLines);
            
            var lines = controller.GetByOrder(orderId);

            controller.OrderLineRepositoryMock.VerifyAll();
            Assert.Equal(expectedLines, lines);
        }

        [Fact]
        public void ShouldCreateOrderLineWithCurrentProductPrice()
        {                     
            var controller = CreateTestableOrderLinesController();
            var line = new OrderLine();   
            controller.OrderLineRepositoryMock.Setup(r => r.Add(line));
            controller.ProductRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(new Product());

            controller.Post(line);

            controller.OrderLineRepositoryMock.VerifyAll();
        }

        [Fact]
        public void ShouldUseCurrentProductPriceForOrderLine()
        {
            var controller = CreateTestableOrderLinesController();
            var productId = Random.Next();
            var line = new OrderLine { ProductId = productId };
            var product = new Product { ProductId = productId, Price = (decimal)Random.NextDouble() };            
            controller.ProductRepositoryMock.Setup(r => r.GetById(productId)).Returns(product);

            controller.Post(line);

            controller.ProductRepositoryMock.VerifyAll();
            controller.OrderLineRepositoryMock.Verify(r => r.Add(It.Is<OrderLine>(ol => ol.Price == product.Price)));                              
        }

        [Fact]
        public void ShouldAddBothProductsFromSalesCombination()
        {
            var controller = CreateTestableOrderLinesController();

            var mainProduct = new Product { ProductId = Random.Next(), Price = Random.Next() };
            var subProduct = new Product { ProductId = Random.Next(), Price = Random.Next() };

            var salesCombination = new SalesCombination
            {
                SalesCombinationId = Random.Next(),
                MainProductId = mainProduct.ProductId,
                SubProductId = subProduct.ProductId,
                Discount = Random.Next()
            };

            controller.SalesCombinationRepositoryMock.Setup(r => r.GetById(salesCombination.SalesCombinationId))
                .Returns(salesCombination);
            controller.ProductRepositoryMock.Setup(r => r.GetById(salesCombination.MainProductId)).Returns(mainProduct);
            controller.ProductRepositoryMock.Setup(r => r.GetById(salesCombination.SubProductId)).Returns(subProduct);            
            
            controller.AddSalesCombination(Random.Next(), salesCombination.SalesCombinationId);

            controller.SalesCombinationRepositoryMock.VerifyAll();
            controller.ProductRepositoryMock.VerifyAll();
            controller.OrderLineRepositoryMock.Verify(r => r.Add(It.Is<OrderLine>(l => l.ProductId == mainProduct.ProductId)));
            controller.OrderLineRepositoryMock
                .Verify(r => r.Add(It.Is<OrderLine>(l => l.ProductId == subProduct.ProductId && l.Price == subProduct.Price - salesCombination.Discount)));
        }

        [Fact]
        public void ShouldIncreaseQuantityWhenOrderAlreadyHasProduct()
        {
            var controller = CreateTestableOrderLinesController();
            var product = new Product { ProductId = Random.Next(), Price = (decimal)Random.NextDouble() }; 
            var line = new OrderLine { OrderId = Random.Next(), ProductId = product.ProductId , Quantity = 1 };
            var lines = new List<OrderLine> { 
                new OrderLine { OrderLineId = line.OrderId, ProductId = line.ProductId, Quantity = 1 } 
            };

            controller.ProductRepositoryMock.Setup(r => r.GetById(line.ProductId)).Returns(product);
            controller.OrderLineRepositoryMock.Setup(r => r.GetByOrder(line.OrderId)).Returns(lines);
            controller.OrderLineRepositoryMock.Setup(r => r.Update(It.Is<OrderLine>(l =>
                l.OrderId == line.OrderId && l.ProductId == line.ProductId && l.Quantity == 2)));

            controller.Post(line);

            controller.OrderLineRepositoryMock.VerifyAll();
        }

        [Fact]
        public void ShouldNotAddBothProductsFromSalesCombinationIfOneProductAlreadyAdded()
        {
            var controller = CreateTestableOrderLinesController();
            int orderId = Random.Next();
            var mainProduct = new Product { ProductId = Random.Next(), Price = Random.Next() };
            var subProduct = new Product { ProductId = Random.Next(), Price = Random.Next() };

            var salesCombination = new SalesCombination
            {
                SalesCombinationId = Random.Next(),
                MainProductId = mainProduct.ProductId,
                SubProductId = subProduct.ProductId,
                Discount = Random.Next()
            };

            var lines = new[] { new OrderLine { ProductId = mainProduct.ProductId, Quantity = 1 } };

            controller.SalesCombinationRepositoryMock.Setup(r => r.GetById(salesCombination.SalesCombinationId))
                      .Returns(salesCombination);
            controller.OrderLineRepositoryMock.Setup(r => r.GetByOrder(orderId)).Returns(lines);            
            controller.ProductRepositoryMock.Setup(r => r.GetById(salesCombination.SubProductId)).Returns(subProduct);

            controller.AddSalesCombination(orderId, salesCombination.SalesCombinationId);

            controller.SalesCombinationRepositoryMock.VerifyAll();
            controller.ProductRepositoryMock.VerifyAll();
            controller.OrderLineRepositoryMock.Verify(r => r.Add(It.Is<OrderLine>(l => l.ProductId == subProduct.ProductId)));
            controller.ProductRepositoryMock.Verify(r => r.GetById(mainProduct.ProductId), Times.Never());
            controller.OrderLineRepositoryMock
                .Verify(r => r.Add(It.Is<OrderLine>(l => l.ProductId == subProduct.ProductId && l.Price == subProduct.Price - salesCombination.Discount)));            
        }

        private TestableOrderLinesController CreateTestableOrderLinesController()
        {
            var lineRepositoryMock = new Mock<IOrderLineRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            var salesRepositoryMock = new Mock<ISalesCombinationRepository>();
            return new TestableOrderLinesController(lineRepositoryMock, productRepositoryMock, salesRepositoryMock);
        }
    }

    internal class TestableOrderLinesController : OrderLinesController
    {
        public TestableOrderLinesController(
            Mock<IOrderLineRepository> lineRepositoryMock,
            Mock<IProductRepository> productRepositoryMock,
            Mock<ISalesCombinationRepository> salesRepositoryMock)
            : base(lineRepositoryMock.Object, productRepositoryMock.Object, salesRepositoryMock.Object)
        {
            OrderLineRepositoryMock = lineRepositoryMock;
            ProductRepositoryMock = productRepositoryMock;
            SalesCombinationRepositoryMock = salesRepositoryMock;
        }

        public Mock<IOrderLineRepository> OrderLineRepositoryMock { get; private set; }
        public Mock<IProductRepository> ProductRepositoryMock { get; private set; }
        public Mock<ISalesCombinationRepository> SalesCombinationRepositoryMock { get; private set; }
    }
}
