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

        [Given(@"there are following customers in the shop")]
        public void GivenThereAreFollowingCustomersInTheShop(Table table)
        {
            DatabaseHelper.CreateCustomersTable();
            var customers = table.CreateSet(BuildCustomer);

            foreach (var customer in customers)
                DatabaseHelper.Save(customer);
        }

        [When(@"I add following customer")]
        public void WhenIAddFollowingCustomer(Table table)
        {
            var customer = table.CreateInstance<Customer>();
            customersApi.Post(customer);
        }

        [When(@"I search for recurring customer '(.*)'")]
        public void WhenISearchForRecurringCustomer(string name)
        {
            customers = customersApi.Get(name);
        }

        [When(@"I edit details of this customer")]
        public void WhenIEditDetailsOfThisCustomer(Table table)
        {
            var customer = customersApi.Get(customerId);
            table.FillInstance(customer);
            customersApi.Put(customer);
        }

        [Then(@"I see only these customers")]
        public void ThenISeeOnlyTheseCustomers(Table table)
        {
            AssertCustomersAreEqual(table.CreateSet<Customer>(), customers);
        }


        [Then(@"I see following customers")]
        public void ThenISeeFollowingCustomers(Table table)
        {
            AssertCustomersAreEqual(table.CreateSet<Customer>(), customersApi.Get());
        }

        private void AssertCustomersAreEqual(
            IEnumerable<Customer> expectedCustomers, IEnumerable<Customer> actualCustomers)
        {
            Assert.Equal(expectedCustomers.Count(), actualCustomers.Count());

            foreach (var expectedCustomer in expectedCustomers)
            {
                var actualCustomer = actualCustomers.First(c => c.EmailAddress == expectedCustomer.EmailAddress);
                Assert.Equal(expectedCustomer.FirstName, actualCustomer.FirstName);
                Assert.Equal(expectedCustomer.LastName, actualCustomer.LastName);
            }
        }

        [Then(@"I do not see any customers")]
        public void ThenIDoNotSeeAnyCustomers()
        {
            Assert.Equal(0, customers.Count);
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

        [Given(@"customer without orders")]
        public void GivenCustomerWithoutOrders()
        {
            DatabaseHelper.CreateOrdersTable();
            DatabaseHelper.CreateCustomersTable();
            var customer = new Customer { FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@gmail.com" };
            customersApi.Post(customer);
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

        private Customer BuildCustomer()
        {
            return Builder<Customer>.CreateNew()
                .With(c => c.PostalCode = "123456")
                .With(c => c.HouseNumber = "1")
                .Build();
        }
    }
}
