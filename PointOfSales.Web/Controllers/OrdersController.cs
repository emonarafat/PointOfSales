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

        public OrdersController(IOrderRepository orderRepository)
        {            
            this.orderRepository = orderRepository;
        }
        public Order Get(int id)
        {
            return orderRepository.GetById(id);
        }
    }
}
