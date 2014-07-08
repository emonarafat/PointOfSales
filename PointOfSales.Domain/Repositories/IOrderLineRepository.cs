using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Domain.Repositories
{
    public interface IOrderLineRepository
    {
        IEnumerable<OrderLine> GetByOrder(int orderId);

        void Add(OrderLine line);
        void Update(OrderLine line);
    }
}
