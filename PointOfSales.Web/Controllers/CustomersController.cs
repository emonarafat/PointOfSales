using PointOfSales.Domain.Model;
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
        public IEnumerable<Customer> Get()
        {
            return Enumerable.Empty<Customer>();
        }

        public void Post(Customer customer)
        {

        }
    }
}
