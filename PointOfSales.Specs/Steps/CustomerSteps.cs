using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace PointOfSales.Specs.Steps
{
    [Binding]
    public class CustomerSteps
    {
        [Given(@"I don't have any customers")]
        public void GivenIDonTHaveAnyCustomers()
        {
            DatabaseHelper.CreateCustomersTable();
        }

        [When(@"I add customer")]
        public void WhenIAddCustomer()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@gmail.com" };
            WebApiHelper.Post("api/customers", customer);
        }

        [Then(@"customer should exist in the system")]
        public void ThenCustomerShouldExistInTheSystem()
        {
            var customers = WebApiHelper.Get<List<Customer>>("api/customers");
            Assert.Equal(1, customers.Count);
            Assert.Equal("john.doe@gmail.com", customers[0].EmailAddress);
        }
    }
}
