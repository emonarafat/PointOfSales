using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Specs
{
    public class OrdersApi : WebApiWrapper
    {
        public Order Get(int id)
        {
            return Get<Order>("api/orders/{0}", id);
        }

        public List<Order> GetCustomerOrders(int customerId)
        {
            return Get<List<Order>>("api/customers/{0}/orders", customerId);
        }

        public void Post(Order order)
        {
            Post("api/orders", order);
        }
    }
}
