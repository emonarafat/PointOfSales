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
    public class OrdersController : ApiController
    {
        private IOrderRepository orderRepository;
        private IOrderLineRepository orderLineRepository;

        public OrdersController(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository)
        {            
            this.orderRepository = orderRepository;
            this.orderLineRepository = orderLineRepository;
        }

        public Order Get(int id)
        {
            var order = orderRepository.GetById(id);
            if (order == null)
                return null;

            var lines = orderLineRepository.GetByOrder(id);
            order.TotalPrice = lines.Sum(l => l.Price);
            return order;
        }

        public void Post(Order order)
        {
            orderRepository.Add(order);
        }
    }
}
