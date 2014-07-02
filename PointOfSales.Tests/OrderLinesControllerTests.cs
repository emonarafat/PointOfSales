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
            int orderId = Random.Next();
            var expectedLines = Enumerable.Empty<OrderLine>();
            var linesRepositoryMock = new Mock<IOrderLineRepository>();
            var productRepositoryMock = new Mock<IProductRepository>();
            linesRepositoryMock.Setup(r => r.GetByOrder(orderId)).Returns(expectedLines);
            var controller = new OrderLinesController(linesRepositoryMock.Object, productRepositoryMock.Object);
            
            var lines = controller.GetByOrder(orderId);

            linesRepositoryMock.VerifyAll();
            Assert.Equal(expectedLines, lines);
        }

        [Fact]
        public void ShouldCreateOrderLineWithCurrentProductPrice()
        {
            var line = new OrderLine();
            var linesRepositoryMock = new Mock<IOrderLineRepository>();
            linesRepositoryMock.Setup(r => r.Add(line));
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(new Product());
            var controller = new OrderLinesController(linesRepositoryMock.Object, productRepositoryMock.Object);

            controller.Post(line);

            linesRepositoryMock.VerifyAll();
        }

        [Fact]
        public void ShouldUseCurrentProductPriceForOrderLine()
        {            
            var productId = Random.Next();
            var line = new OrderLine { ProductId = productId };
            var product = new Product { ProductId = productId, Price = (decimal)Random.NextDouble() };

            var lineRepositoryMock = new Mock<IOrderLineRepository>();            
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(r => r.GetById(productId)).Returns(product);
            var controller = new OrderLinesController(lineRepositoryMock.Object, productRepositoryMock.Object);

            controller.Post(line);

            productRepositoryMock.VerifyAll();
            lineRepositoryMock.Verify(r => r.Add(It.Is<OrderLine>(ol => ol.Price == product.Price)));                              
        }
        
        // Should update quantity and price if product already added
    }
}
