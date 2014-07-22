using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Specs
{
    public class OrderLinesApi
    {
        public List<OrderLine> GetOrderLines(int orderId)
        {
            return WebApiHelper.Get<List<OrderLine>>("api/orders/{0}/lines", orderId);
        }

        public void AddOrderLine(int orderId, int productId, int quantity)
        {
            WebApiHelper.Post("api/orders/{0}/lines?productId={1}&quantity={2}", orderId, productId, quantity);
        }
    }
}
