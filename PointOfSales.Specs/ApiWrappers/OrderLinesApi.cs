using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Specs
{
    public class OrderLinesApi : WebApiWrapper
    {
        public List<OrderLine> GetOrderLines(int orderId)
        {
            return Get<List<OrderLine>>("api/orders/{0}/lines", orderId);
        }

        public void AddOrderLine(int orderId, int productId, int quantity)
        {
            Post("api/orders/{0}/lines?productId={1}&quantity={2}", orderId, productId, quantity);
        }
    }
}
