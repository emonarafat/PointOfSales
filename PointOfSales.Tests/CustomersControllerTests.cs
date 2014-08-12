using Moq;
using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using PointOfSales.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
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
            var customer = new Customer();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(r => r.Add(customer)).Returns(customer);
            var controller = new CustomersController(customerRepositoryMock.Object);
            controller.Request = Mock.Of<HttpRequestMessage>();
            controller.Configuration = Mock.Of<HttpConfiguration>();

            var response = controller.Post(customer);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Customer actualCustomer;
            Assert.True(response.TryGetContentValue(out actualCustomer));
            Assert.Equal(customer, actualCustomer);
            customerRepositoryMock.VerifyAll();
        }

        [Fact]
        public void ShouldSearchRecurringCustomers()
        {
            string search = "foo";
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedCustomers = Enumerable.Empty<Customer>();
            customerRepositoryMock.Setup(r => r.GetByName(search)).Returns(expectedCustomers);
            var controller = new CustomersController(customerRepositoryMock.Object);

            var actualCustomers = controller.Get("foo");

            customerRepositoryMock.VerifyAll();
            Assert.Equal(expectedCustomers, actualCustomers);
        }

        [Fact]
        public void ShouldReturnNotFoundWhenCustomerNotFound()
        {
            var id = new Random().Next();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(r => r.GetById(id)).Returns((Customer)null);
            var controller = new CustomersController(customerRepositoryMock.Object);

            var exception = Assert.Throws<HttpResponseException>(() => controller.Get(id));
            Assert.Equal(HttpStatusCode.NotFound, exception.Response.StatusCode);
            customerRepositoryMock.VerifyAll();
        }

        [Fact]
        public void ShouldReturnExistingCustomer()
        {
            var id = new Random().Next();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedCustomer = new Customer();
            customerRepositoryMock.Setup(r => r.GetById(id)).Returns(expectedCustomer);
            var controller = new CustomersController(customerRepositoryMock.Object);

            var actualCustomer = controller.Get(id);

            Assert.Equal(expectedCustomer, actualCustomer);
            customerRepositoryMock.VerifyAll();
        }

        [Fact]
        public void ShouldUpdateExistingCustomer()
        {
            var customerId = new Random().Next();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var expectedCustomer = new Customer();
            customerRepositoryMock.Setup(r => r.Update(expectedCustomer)).Returns(true);
            var controller = new CustomersController(customerRepositoryMock.Object);

            controller.Put(customerId, expectedCustomer);

            customerRepositoryMock.VerifyAll();
        }

        [Fact]
        public void ShouldReturnNotFoundWhenUpdatingNonExistingCustomer()
        {
            var id = new Random().Next();
            var customer = new Customer();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            customerRepositoryMock.Setup(r => r.Update(customer)).Returns(false);
            var controller = new CustomersController(customerRepositoryMock.Object);

            var exception = Assert.Throws<HttpResponseException>(() => controller.Put(id, customer));
            Assert.Equal(HttpStatusCode.NotFound, exception.Response.StatusCode);
            customerRepositoryMock.VerifyAll();
        }
    }
}
