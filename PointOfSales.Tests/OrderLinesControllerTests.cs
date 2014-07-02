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
            var repositoryMock = new Mock<IOrderLineRepository>();            
            repositoryMock.Setup(r => r.GetByOrder(orderId)).Returns(expectedLines);
            var controller = new OrderLinesController(repositoryMock.Object);

            var lines = controller.GetByOrder(orderId);

            repositoryMock.VerifyAll();
            Assert.Equal(expectedLines, lines);
        }

        [Fact]
        public void ShouldCreateOrderLineWithCurrentProductPrice()
        {
            var line = new OrderLine();
            var repositoryMock = new Mock<IOrderLineRepository>();
            repositoryMock.Setup(r => r.Add(line));
            var controller = new OrderLinesController(repositoryMock.Object);            

            controller.Post(line);

            repositoryMock.VerifyAll();
        }
    }
}
