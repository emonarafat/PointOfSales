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
        private CustomersApi customersApi;
        private List<Customer> customers;
        private List<Order> orders;

        public CustomerSteps(CustomersApi customersApi)
        {
            this.customersApi = customersApi;
        }

        [Given(@"I don't have any customers")]
        public void GivenIDonTHaveAnyCustomers()
        {
            DatabaseHelper.CreateCustomersTable();
        }

        [When(@"I add customer")]
        public void WhenIAddCustomer()
        {
            var customer = new Customer { FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@gmail.com" };
            customersApi.Post(customer);
        }

        [Then(@"customer should exist in the system")]
        public void ThenCustomerShouldExistInTheSystem()
        {
            var customers = customersApi.Get();
            Assert.Equal(1, customers.Count);
            Assert.Equal("john.doe@gmail.com", customers[0].EmailAddress);
        }

        [Given(@"customer without orders")]
        public void GivenCustomerWithoutOrders()
        {            
            DatabaseHelper.CreateOrdersTable();
            DatabaseHelper.CreateCustomersTable();
            var customer = new Customer { FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@gmail.com" };
            customersApi.Post(customer);
        }

        [Then(@"I do not see any customers")]
        public void ThenIDoNotSeeAnyCustomers()
        {
            Assert.Equal(0, customers.Count);
        }

        [Given(@"cusomer with (.*) orders")]
        public void GivenCusomerWithOrders(int ordersCount)
        {
            DatabaseHelper.CreateOrdersTable();
            DatabaseHelper.CreateCustomersTable();
            var customer = new Customer { FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@gmail.com" };
            int id = customersApi.PostAndReturnId(customer);

            for (int i = 0; i < ordersCount; i++)
                WebApiHelper.Post("api/orders", new Order { CustomerId = id });
        }

        [Then(@"I see (.*) customer")]
        public void ThenISeeCustomer(int customersCount)
        {
            Assert.Equal(1, customers.Count);
        }

        [When(@"I view purchase history")]
        public void WhenIViewPurchaseHistory()
        {
            orders = WebApiHelper.Get<List<Order>>("api/customers/1/orders");
        }

        [Then(@"I do not see any orders")]
        public void ThenIDoNotSeeAnyOrders()
        {
            Assert.Equal(0, orders.Count);
        }

        [Then(@"I see (.*) orders")]
        public void ThenISeeOrders(int ordersCount)
        {
            Assert.Equal(ordersCount, orders.Count);
        }

        [Given(@"I have no customers")]
        public void GivenIHaveNoCustomers()
        {
            DatabaseHelper.CreateCustomersTable();
        }

        [Given(@"I have some customers")]
        public void GivenIHaveSomeCustomers()
        {
            DatabaseHelper.CreateCustomersTable();
            DatabaseHelper.SeedCustomers();
        }

        [When(@"I search for recurring customer '(.*)'")]
        public void WhenISearchForRecurringCustomer(string name)
        {
            customers = customersApi.Get(name);
        }

        [Then(@"I see all customers with names containing search string")]
        public void ThenISeeAllCustomersWithNamesContainingSearchString()
        {
            // TODO: Table input?
            Assert.Equal(4, customers.Count);
        }

        [When(@"I edit details of a customer")]
        public void WhenIEditDetailsOfACustomer()
        {
            var customer = customersApi.Get(1);
            customer.FirstName = "Jack";
            customer.LastName = "Daniels";
            customer.MiddleName = null;
            customer.EmailAddress = "jack.daniels@gmail.com";
            customer.City = "Chicago";
            customersApi.Put(customer);
        }

        [Then(@"I should see updated customer")]
        public void ThenIShouldSeeUpdatedCustomer()
        {
            var customer = customersApi.Get(1);
            Assert.Equal("Jack", customer.FirstName);
            Assert.Equal("Daniels", customer.LastName);
            Assert.Null(customer.MiddleName);
            Assert.Equal("jack.daniels@gmail.com", customer.EmailAddress);
            Assert.Equal("Chicago", customer.City);
        }
    }
}
