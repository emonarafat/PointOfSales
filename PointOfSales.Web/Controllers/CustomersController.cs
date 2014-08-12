using NLog;
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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CustomersController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [Route("")]
        public IEnumerable<Customer> Get()
        {
            Logger.Info("Getting all customers");
            return customerRepository.GetAll();
        }

        [Route("{id:int}")]
        public Customer Get(int id)
        {
            Logger.Info("Getting customer by id equal '{0}'", id);
            var customer = customerRepository.GetById(id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return customer;
        }
        
        [Route("")]
        public HttpResponseMessage Post(Customer customer)
        {
            Logger.Info("Adding customer");
            var addedCustomer = customerRepository.Add(customer);
            return Request.CreateResponse(HttpStatusCode.Created, addedCustomer);
        }

        [Route("{id:int}")]
        public void Put(int id, Customer customer)
        {
            Logger.Info("Updating customer");
            customer.CustomerId = id;

            if(!customerRepository.Update(customer))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [Route("")]
        public IEnumerable<Customer> Get(string name)
        {
            Logger.Info("Searching customers by name containing '{0}'", name);
            return customerRepository.GetByName(name);
        }
    }
}
