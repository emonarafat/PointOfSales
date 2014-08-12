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
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private ICustomerRepository customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [Route("")]
        public IEnumerable<Customer> Get()
        {
            return customerRepository.GetAll();
        }

        [Route("{id:int}")]
        public Customer Get(int id)
        {
            var customer = customerRepository.GetById(id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }
        
        [Route("")]
        public HttpResponseMessage Post(Customer customer)
        {
            var addedCustomer = customerRepository.Add(customer);
            return Request.CreateResponse(HttpStatusCode.Created, addedCustomer);
        }

        [Route("{id:int}")]
        public void Put(int id, Customer customer)
        {
            customer.CustomerId = id;

            if(!customerRepository.Update(customer))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [Route("")]
        public IEnumerable<Customer> Get(string name)
        {
            return customerRepository.GetByName(name);
        }
    }
}
