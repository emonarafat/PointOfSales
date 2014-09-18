using Moq;
using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using PointOfSales.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void ShouldUseCurrentProductPriceForOrderLine()
        {
            var controller = CreateTestableOrderLinesController();
            var productId = Random.Next();
            var line = new OrderLine { ProductId = productId };
            var product = new Product { ProductId = productId, Price = (decimal)Random.NextDouble() };

            controller.SalesCombinationRepositoryMock.Setup(r => r.GetByProductId(productId))
                .Returns(Enumerable.Empty<SalesCombination>());
            controller.ProductRepositoryMock.Setup(r => r.GetById(productId)).Returns(product);

            controller.Post(line);

            controller.ProductRepositoryMock.VerifyAll();
            controller.OrderLineRepositoryMock.Verify(r => r.Add(It.Is<OrderLine>(ol => ol.Price == product.Price)));
        }

        [Fact]
        public void ShouldAddDiscountIfSalesCombinnationExistsForCurrentProduct()
        {
            var controller = CreateTestableOrderLinesController();
            var mainProductId = Random.Next();
            var subProductId = Random.Next();

            var line = new OrderLine { OrderId = Random.Next(), ProductId = mainProductId };

            var salesCombination = new SalesCombination
            {
                MainProductId = mainProductId,
                SubProductId = subProductId,
                Discount = 10 * (decimal)Random.NextDouble()
            };

            var mainProduct = new Product { ProductId = mainProductId, Price = 4 * salesCombination.Discount };

            controller.OrderLineRepositoryMock.Setup(r => r.GetByOrder(line.OrderId))
                .Returns(new []{ new OrderLine { ProductId = subProductId, Price = 2 * salesCombination.Discount }});
            controller.ProductRepositoryMock.Setup(r => r.GetById(mainProductId)).Returns(mainProduct);
            controller.SalesCombinationRepositoryMock.Setup(r => r.GetByProductId(mainProductId))
                .Returns(new[] { salesCombination });

            controller.Post(line);

            controller.OrderLineRepositoryMock.VerifyAll();
            controller.SalesCombinationRepositoryMock.VerifyAll();
            controller.OrderLineRepositoryMock.Verify(r => r.Add(It.Is<OrderLine>(l =>
                l.ProductId == mainProductId && l.Price == mainProduct.Price - salesCombination.Discount)));
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
                new OrderLine { OrderLineId = Random.Next(), ProductId = line.ProductId, Quantity = 1 } 
            };

            controller.ProductRepositoryMock.Setup(r => r.GetById(line.ProductId)).Returns(product);
            controller.OrderLineRepositoryMock.Setup(r => r.GetByOrder(line.OrderId)).Returns(lines);
            controller.OrderLineRepositoryMock.Setup(r => r.Update(It.Is<OrderLine>(l =>
                l.OrderLineId == lines[0].OrderLineId && l.ProductId == line.ProductId && l.Quantity == 2)));

            controller.Post(line);

            controller.OrderLineRepositoryMock.VerifyAll();
        }

        [Fact]
        public void ShouldAddBothProductsFromSalesCombinationIfOneProductAlreadyAdded()
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
            controller.OrderLineRepositoryMock.Verify(r => r.Update(It.Is<OrderLine>(l => l.ProductId == mainProduct.ProductId && l.Quantity == 2)));
            controller.OrderLineRepositoryMock
                .Verify(r => r.Add(It.Is<OrderLine>(l => l.ProductId == subProduct.ProductId && l.Price == subProduct.Price - salesCombination.Discount)));
        }

        [Fact]
        public void ShouldNotAddAnyProductsFromSalesCombinationIfBothAlreadyAdded()
        {
            var controller = CreateTestableOrderLinesController();
            int orderId = Random.Next();

            var salesCombination = new SalesCombination
            {
                SalesCombinationId = Random.Next(),
                MainProductId = Random.Next(),
                SubProductId = Random.Next()
            };

            var lines = new[] {
                new OrderLine { ProductId = salesCombination.MainProductId, Quantity = 1 },
                new OrderLine { ProductId = salesCombination.SubProductId, Quantity = 1 }
            };

            controller.SalesCombinationRepositoryMock
                      .Setup(r => r.GetById(salesCombination.SalesCombinationId))
                      .Returns(salesCombination);

            controller.OrderLineRepositoryMock.Setup(r => r.GetByOrder(orderId)).Returns(lines);

            controller.AddSalesCombination(orderId, salesCombination.SalesCombinationId);

            controller.SalesCombinationRepositoryMock.VerifyAll();
            controller.ProductRepositoryMock.Verify(r => r.GetById(It.IsAny<int>()), Times.Never());
            controller.OrderLineRepositoryMock.VerifyAll();
            controller.OrderLineRepositoryMock
                .Verify(r => r.Update(It.Is<OrderLine>(l => l.ProductId == salesCombination.MainProductId && l.Quantity == 2)));

            controller.OrderLineRepositoryMock
                .Verify(r => r.Update(It.Is<OrderLine>(l => l.ProductId == salesCombination.SubProductId && l.Quantity == 2)));
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
