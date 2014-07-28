using FizzWare.NBuilder;
using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace PointOfSales.Specs.Steps
{
    [Binding]
    public class CustomerSteps
    {
        private CustomersApi customersApi;
        private OrdersApi ordersApi;
        private List<Customer> customers;
        private List<Order> orders;
        private int customerId;

        public CustomerSteps(CustomersApi customersApi, OrdersApi ordersApi)
        {
            this.customersApi = customersApi;
            this.ordersApi = ordersApi;
        }

        [Given(@"there are no customers in the shop")]
        public void GivenThereAreNoCustomersInTheShop()
        {
            DatabaseHelper.CreateCustomersTable();
        }

        [Given(@"there is following customer in shop")]
        public void GivenThereIsFollowingCustomerInShop(Table table)
        {
            DatabaseHelper.CreateCustomersTable();
            var customer = table.CreateInstance<Customer>();
            customer.EntryDate = DateTime.Today;
            customerId = DatabaseHelper.Save(customer);
        }

        [When(@"I add following customer")]
        public void WhenIAddFollowingCustomer(Table table)
        {
            var customer = table.CreateInstance<Customer>();
            customersApi.Post(customer);
        }

        [Then(@"I see following customers")]
        public void ThenISeeFollowingCustomers(Table table)
        {
            var customers = customersApi.Get().ToDictionary(c => c.EmailAddress);
            var expectedCustomers = table.CreateSet<Customer>().ToList();
            Assert.Equal(expectedCustomers.Count, customers.Count);

            foreach(var expectedCustomer in expectedCustomers)
            {
                var actualCustomer = customers[expectedCustomer.EmailAddress];
                Assert.Equal(expectedCustomer.FirstName, actualCustomer.FirstName);
                Assert.Equal(expectedCustomer.LastName, actualCustomer.LastName);
            }
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
                ordersApi.Post(new Order { CustomerId = id });
        }

        [Then(@"I see exactly (.*) customers")]
        public void ThenISeeCustomer(int customersCount)
        {
            Assert.Equal(1, customers.Count);
        }

        [When(@"I view purchase history")]
        public void WhenIViewPurchaseHistory()
        {
            orders = ordersApi.GetCustomerOrders(1);
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

        [When(@"I edit details of this customer")]
        public void WhenIEditDetailsOfThisCustomer(Table table)
        {
            var customer = customersApi.Get(customerId);
            table.FillInstance(customer);
            customersApi.Put(customer);
        }

        [Then(@"I see updated details of this customer")]
        public void ThenISeeUpdatedDetailsOfThisCustomer(Table table)
        {
            var customer = customersApi.Get(customerId);
            var expectedCustomer = table.CreateInstance<Customer>();

            Assert.Equal(expectedCustomer.FirstName, customer.FirstName);
            Assert.Equal(expectedCustomer.LastName, customer.LastName);
            Assert.Equal(expectedCustomer.MiddleName, customer.MiddleName);
            Assert.Equal(expectedCustomer.EmailAddress, customer.EmailAddress);
            Assert.Equal(expectedCustomer.City, customer.City);
            Assert.Equal(expectedCustomer.Street, customer.Street);
            Assert.Equal(expectedCustomer.HouseNumber, customer.HouseNumber);
            Assert.Equal(expectedCustomer.PostalCode, customer.PostalCode);
        }
    }
}
