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
    public class OrderLinesController : ApiController
    {
        private IOrderLineRepository orderLineRepository;

        public OrderLinesController(IOrderLineRepository orderLineRepository)
        {            
            this.orderLineRepository = orderLineRepository;
        }

        public IEnumerable<OrderLine> GetByOrder(int orderId)
        {
            return orderLineRepository.GetByOrder(orderId);
        }

        public void Post([FromBody]OrderLine line)
        {
            orderLineRepository.Add(line);
        }
    }
}
