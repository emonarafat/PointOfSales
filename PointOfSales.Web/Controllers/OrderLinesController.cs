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
        private IProductRepository productRepository;
        private ISalesCombinationRepository salesCombinationRepository;

        public OrderLinesController(IOrderLineRepository orderLineRepository, IProductRepository productRepository, ISalesCombinationRepository salesCombinationRepository)
        {            
            this.orderLineRepository = orderLineRepository;
            this.productRepository = productRepository;
            this.salesCombinationRepository = salesCombinationRepository;
        }

        public IEnumerable<OrderLine> GetByOrder(int orderId)
        {
            return orderLineRepository.GetByOrder(orderId);
        }

        public void Post([FromBody]OrderLine line)
        {
            var product = productRepository.GetById(line.ProductId);
            line.Price = product.Price;
            orderLineRepository.Add(line);
        }

        [AcceptVerbs("POST")]
        public void AddSalesCombination(int orderId, int salesCombinationId)
        {
            var sales = salesCombinationRepository.GetById(salesCombinationId);
            // TODO: UoW
            var mainProduct = productRepository.GetById(sales.MainProductId);
            orderLineRepository.Add(new OrderLine { ProductId = mainProduct.ProductId, Price = mainProduct.Price, Quantity = 1, OrderId = orderId });
            var subProduct = productRepository.GetById(sales.SubProductId);
            orderLineRepository.Add(new OrderLine { ProductId = subProduct.ProductId, Price = subProduct.Price, Quantity = 1, OrderId = orderId });
        }
    }
}
