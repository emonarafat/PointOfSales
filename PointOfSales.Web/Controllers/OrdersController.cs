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
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private IOrderRepository orderRepository;
        private IOrderLineRepository orderLineRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public OrdersController(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository)
        {            
            this.orderRepository = orderRepository;
            this.orderLineRepository = orderLineRepository;
        }

        [Route("{id:int}")]
        public Order Get(int id)
        {
            Logger.Info("Getting order by id equal '{0}'", id);
            var order = orderRepository.GetById(id);
            if (order == null)
                return null;

            var lines = orderLineRepository.GetByOrder(id);
            order.TotalPrice = lines.Sum(l => l.Price * l.Quantity);
            return order;
        }
        
        [Route("~/api/customers/{customerId}/orders")]
        public IEnumerable<Order> GetByCustomer(int customerId)
        {
            Logger.Info("Getting orders of customer '{0}'", customerId);
            return orderRepository.GetByCustomer(customerId);
        }

        [Route("")]
        public void Post(Order order)
        {
            Logger.Info("Adding order");
            orderRepository.Add(order);
        }
    }
}
