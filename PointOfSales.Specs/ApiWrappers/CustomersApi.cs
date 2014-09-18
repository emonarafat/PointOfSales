using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Specs
{
    public class CustomersApi : WebApiWrapper
    {
        public List<Customer> Get()
        {
            return Get<List<Customer>>("api/customers");
        }

        public Customer Get(int id)
        {
            return Get<Customer>("api/customers/" + id);
        }

        public List<Customer> Get(string name)
        {
            return Get<List<Customer>>("api/customers?name={0}", name);
        }

        public void Post(Customer customer)
        {
            Post("api/customers", customer);
        }

        public int PostAndReturnId(Customer customer)
        {
            return PostAndReturnId("api/customers", customer);
        }

        public void Put(Customer customer)
        {
            Put("api/customers/" + customer.CustomerId, customer);
        }
    }
}
