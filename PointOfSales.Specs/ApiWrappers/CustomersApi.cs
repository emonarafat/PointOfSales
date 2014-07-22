using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Specs
{
    public class CustomersApi
    {
        public List<Customer> Get()
        {
            return WebApiHelper.Get<List<Customer>>("api/customers");
        }

        public Customer Get(int id)
        {
            return WebApiHelper.Get<Customer>("api/customers/" + id);
        }

        public List<Customer> Get(string name)
        {
            return WebApiHelper.Get<List<Customer>>("api/customers?name={0}", name);
        }

        public void Post(Customer customer)
        {
            WebApiHelper.Post("api/customers", customer);
        }

        public int PostAndReturnId(Customer customer)
        {
            return WebApiHelper.PostAndReturnId("api/customers", customer);
        }

        public void Put(Customer customer)
        {
            WebApiHelper.Put("api/customers/" + customer.CustomerId, customer);
        }
    }
}
