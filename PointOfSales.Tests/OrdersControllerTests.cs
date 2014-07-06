using Moq;
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
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var controller = new OrdersController();
        }
    }
}
