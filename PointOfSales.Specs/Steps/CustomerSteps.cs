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
        private List<Customer> actualCustomers;

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

        [Given(@"customer without orders")]
        public void GivenCustomerWithoutOrders()
        {
            // TODO: Looks like adding new customer
            DatabaseHelper.CreateOrdersTable();
            DatabaseHelper.CreateCustomersTable();
            var customer = new Customer { FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@gmail.com" };
            WebApiHelper.Post("api/customers", customer);
        }

        [When(@"I search recurring customers")]
        public void WhenISearchRecurringCustomers()
        {
            actualCustomers = WebApiHelper.Get<List<Customer>>("api/customers/recurring");
        }

        [Then(@"I don't see any customers")]
        public void ThenIDonTSeeAnyCustomers()
        {
            Assert.Equal(0, actualCustomers.Count);
        }

        [Given(@"cusomer with (.*) orders")]
        public void GivenCusomerWithOrders(int ordersCount)
        {
            DatabaseHelper.CreateOrdersTable();
            DatabaseHelper.CreateCustomersTable();
            var customer = new Customer { FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@gmail.com" };
            int id = WebApiHelper.PostAndReturnId("api/customers", customer);

            for (int i = 0; i < ordersCount; i++)
                WebApiHelper.Post("api/orders", new Order { CustomerId = id });
        }

        [Then(@"I see (.*) customer")]
        public void ThenISeeCustomer(int customersCount)
        {
            Assert.Equal(1, actualCustomers.Count);
        }

        [When(@"I view purchase history")]
        public void WhenIViewPurchaseHistory()
        {
            actualOrders = WebApiHelper.Get<List<Order>>("api/customers/1/orders");
        }

        private List<Order> actualOrders;

        [Then(@"I do not see any orders")]
        public void ThenIDoNotSeeAnyOrders()
        {
            Assert.Equal(0, actualOrders.Count);
        }
    }
}
