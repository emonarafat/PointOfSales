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
    public class OrdersControllerTests
    {
        private static readonly Random Random = new Random();

        [Fact]
        public void ShouldReturnOrder()
        {
            var expectedOrder = new Order { OrderId = Random.Next() };
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var linesRepositoryMock = new Mock<IOrderLineRepository>();
            orderRepositoryMock.Setup(r => r.GetById(expectedOrder.OrderId)).Returns(expectedOrder);
            var controller = new OrdersController(orderRepositoryMock.Object, linesRepositoryMock.Object);

            var order = controller.Get(expectedOrder.OrderId);

            orderRepositoryMock.VerifyAll();
            Assert.Equal(expectedOrder, order);            
        }

        [Fact]
        public void ShoulCalculateTotalOrderPrice()
        {
            var expectedOrder = new Order { OrderId = Random.Next() };
            var linesRepositoryMock = new Mock<IOrderLineRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var controller = new OrdersController(orderRepositoryMock.Object, linesRepositoryMock.Object);
            orderRepositoryMock.Setup(r => r.GetById(expectedOrder.OrderId)).Returns(expectedOrder);
            linesRepositoryMock.Setup(r => r.GetByOrder(expectedOrder.OrderId))
                .Returns(new[] { 
                    new OrderLine { Price = 100, Quantity = 1 },
                    new OrderLine { Price = 50, Quantity = 1} });

            var order = controller.Get(expectedOrder.OrderId);

            orderRepositoryMock.VerifyAll();
            linesRepositoryMock.VerifyAll();
            Assert.Equal(150, order.TotalPrice);
        }

        [Fact]
        public void ShoulReturnNullWhenOrderNotFound()
        {
            var orderId = Random.Next();
            var linesRepositoryMock = new Mock<IOrderLineRepository>();
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var controller = new OrdersController(orderRepositoryMock.Object, linesRepositoryMock.Object);
            orderRepositoryMock.Setup(r => r.GetById(orderId)).Returns((Order)null);

            var order = controller.Get(orderId);

            orderRepositoryMock.VerifyAll();
            Assert.Null(order);
        }
    }
}
