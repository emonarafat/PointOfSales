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
    public class CustomersControllerTests
    {
        [Fact]
        public void ShouldReturnAllCustomers()
        {            
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedCustomers = new Customer[0];
            customerRepositoryMock.Setup(r => r.GetAll()).Returns(expectedCustomers);
            var controller = new CustomersController(customerRepositoryMock.Object);

            var actualCustomers = controller.Get();

            customerRepositoryMock.VerifyAll();
            Assert.Equal(expectedCustomers, actualCustomers);
        }

        [Fact]
        public void ShouldCreateNewCustomer()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var customer = new Customer();            
            var controller = new CustomersController(customerRepositoryMock.Object);

            controller.Post(customer);

            customerRepositoryMock.Verify(r => r.Add(customer), Times.Once());
        }

        [Fact]
        public void ShouldSearchForRecurringCustomers()
        {
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedCustomers = new Customer[0];
            customerRepositoryMock.Setup(r => r.GetRecurringCustomers()).Returns(expectedCustomers);
            var controller = new CustomersController(customerRepositoryMock.Object);

            var actualCustomers = controller.Recurring();

            customerRepositoryMock.VerifyAll();
            Assert.Equal(expectedCustomers, actualCustomers);
        }
    }
}
