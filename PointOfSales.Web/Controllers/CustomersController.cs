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

        public void Post(Customer customer)
        {

        }
    }
}
