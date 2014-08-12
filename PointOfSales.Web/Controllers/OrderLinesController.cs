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
    public class OrderLinesController : ApiController
    {
        private IOrderLineRepository orderLineRepository;
        private IProductRepository productRepository;
        private ISalesCombinationRepository salesCombinationRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public OrderLinesController(IOrderLineRepository orderLineRepository, IProductRepository productRepository, ISalesCombinationRepository salesCombinationRepository)
        {
            this.orderLineRepository = orderLineRepository;
            this.productRepository = productRepository;
            this.salesCombinationRepository = salesCombinationRepository;
        }

        [Route("api/orders/{orderId:int}/lines")]
        public IEnumerable<OrderLine> GetByOrder(int orderId)
        {
            Logger.Info("Getting lines of order '{0}'", orderId);
            return orderLineRepository.GetByOrder(orderId);
        }

        [Route("api/orders/{orderId:int}/lines")]
        public void Post([FromUri]OrderLine line)
        {
            Logger.Info("Adding order line");
            var product = productRepository.GetById(line.ProductId);
            line.Price = product.Price;

            var lines = orderLineRepository.GetByOrder(line.OrderId);
            var existingLine = lines.FirstOrDefault(l => l.ProductId == line.ProductId);

            if (existingLine == null)
            {
                // TODO: Sales combination should be part of order
                var sales = salesCombinationRepository.GetByProductId(line.ProductId);
                // TODO: What if several products match sales combinations?
                var salesCombination = sales.FirstOrDefault(s =>
                    lines.Any(l => l.ProductId == s.MainProductId || l.ProductId == s.SubProductId));

                if (salesCombination != null)
                    line.Price -= salesCombination.Discount;

                orderLineRepository.Add(line);
                return;
            }

            existingLine.Quantity += line.Quantity;
            orderLineRepository.Update(existingLine);
        }

        [Route("api/orders/{orderId:int}/sales/{salesCombinationId:int}")]
        [HttpPost]
        public void AddSalesCombination(int orderId, int salesCombinationId)
        {
            Logger.Info("Adding sales combination '{0}' to order '{1}'", salesCombinationId, orderId);
            var sales = salesCombinationRepository.GetById(salesCombinationId);
            var lines = orderLineRepository.GetByOrder(orderId);

            var mainProductExists = lines.Any(l => l.ProductId == sales.MainProductId);
            var subProductExists = lines.Any(l => l.ProductId == sales.SubProductId);

            // TODO: What if price changes?
            // TODO: Remove duplication
            // TODO: UoW
            if (mainProductExists)
            {
                var mainProductLine = lines.First(l => l.ProductId == sales.MainProductId);
                mainProductLine.Quantity++;
                orderLineRepository.Update(mainProductLine);
            }
            else
            {
                var mainProduct = productRepository.GetById(sales.MainProductId);
                orderLineRepository.Add(new OrderLine { ProductId = mainProduct.ProductId, Price = mainProduct.Price, Quantity = 1, OrderId = orderId });
            }

            if (subProductExists)
            {
                var subProductLine = lines.First(l => l.ProductId == sales.SubProductId);
                subProductLine.Quantity++;
                orderLineRepository.Update(subProductLine);
            }
            else
            {
                var subProduct = productRepository.GetById(sales.SubProductId);
                orderLineRepository.Add(new OrderLine { ProductId = subProduct.ProductId, Price = subProduct.Price - sales.Discount, Quantity = 1, OrderId = orderId });
            }
        }
    }
}
