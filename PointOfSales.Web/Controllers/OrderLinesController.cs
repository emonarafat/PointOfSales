﻿using PointOfSales.Domain.Model;
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

        [Route("api/orders/{orderId:int}/lines")]
        public IEnumerable<OrderLine> GetByOrder(int orderId)
        {
            return orderLineRepository.GetByOrder(orderId);
        }

        [Route("api/orders/{orderId:int}/lines")]
        public void Post([FromUri]OrderLine line)
        {            
            var product = productRepository.GetById(line.ProductId);
            line.Price = product.Price;

            var lines = orderLineRepository.GetByOrder(line.OrderId);
            var existingLine = lines.FirstOrDefault(l => l.ProductId == line.ProductId);

            if (existingLine == null)
            {
                orderLineRepository.Add(line);
                return;
            }

            existingLine.Quantity += line.Quantity;
            orderLineRepository.Update(existingLine);            
        }

        [AcceptVerbs("POST")]
        public void AddSalesCombination(int orderId, int salesCombinationId)
        {
            var sales = salesCombinationRepository.GetById(salesCombinationId);
            var lines = orderLineRepository.GetByOrder(orderId);

            var mainProductExists = lines.Any(l => l.ProductId == sales.MainProductId);
            var subProductExists = lines.Any(l => l.ProductId == sales.SubProductId);

            if (mainProductExists && subProductExists)
            {
                // TODO: Remove duplication
                var mainProductLine = lines.First(l => l.ProductId == sales.MainProductId);
                mainProductLine.Quantity++;
                orderLineRepository.Update(mainProductLine);

                var subProductLine = lines.First(l => l.ProductId == sales.SubProductId);
                subProductLine.Quantity++;
                orderLineRepository.Update(subProductLine);
                return;
            }

            // TODO: UoW
            if (!mainProductExists)
            {
                var mainProduct = productRepository.GetById(sales.MainProductId);
                orderLineRepository.Add(new OrderLine { ProductId = mainProduct.ProductId, Price = mainProduct.Price, Quantity = 1, OrderId = orderId });
            }

            if (!subProductExists)
            {
                var subProduct = productRepository.GetById(sales.SubProductId);
                orderLineRepository.Add(new OrderLine { ProductId = subProduct.ProductId, Price = subProduct.Price - sales.Discount, Quantity = 1, OrderId = orderId });
            }
        }
    }
}
