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
    public class SalesControllerTests
    {
        private static readonly Random Random = new Random();

        [Fact]
        public void ShouldReturnAvailableSalesCombinationsForProduct()
        {
            int productId = Random.Next();
            var repositoryMock = new Mock<ISalesCombinationRepository>();
            var expectedSales = Enumerable.Empty<SalesCombination>();
            repositoryMock.Setup(r => r.GetByProductId(productId)).Returns(expectedSales);
            var controller = new SalesController(repositoryMock.Object);

            var sales = controller.GetByProduct(productId);

            repositoryMock.VerifyAll();
            Assert.Equal(expectedSales, sales);            
        }
    }
}
