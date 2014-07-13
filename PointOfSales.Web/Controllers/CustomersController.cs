using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PointOfSales.Web.Controllers
{
    public class CustomersController : ApiController
    {
        private ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {            
            this.customerRepository = customerRepository;
        }
        public IEnumerable<Customer> Get()
        {
            return customerRepository.GetAll();
        }

        public Customer Get(int id)
        {
            var customer = customerRepository.GetById(id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }
        
        public int Post(Customer customer)
        {
            return customerRepository.Add(customer);
        }

        public void Put(int id, Customer customer)
        {
            customer.CustomerId = id;

            if(!customerRepository.Update(customer))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpGet]
        public IEnumerable<Customer> Search(string search)
        {
            return customerRepository.GetByName(search);
        }
    }
}
