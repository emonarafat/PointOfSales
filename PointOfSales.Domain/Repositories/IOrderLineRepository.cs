using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Domain.Repositories
{
    public interface IOrderLineRepository
    {
        IEnumerable<OrderLine> GetByOrder(int orderId);

        void Add(OrderLine line);
        void Update(OrderLine line);
    }
}
