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
        [Fact]
        public void ShouldReturnOrder()
        {
            var expectedOrder = new Order { OrderId = new Random().Next() };
            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(r => r.GetById(expectedOrder.OrderId)).Returns(expectedOrder);
            var controller = new OrdersController(orderRepositoryMock.Object);

            var order = controller.Get(expectedOrder.OrderId);

            orderRepositoryMock.VerifyAll();
            Assert.Equal(expectedOrder, order);            
        }
    }
}
