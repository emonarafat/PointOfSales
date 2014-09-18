using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Domain.Repositories
{
    public interface IOrderRepository
    {
        Order GetById(int id);
        void Add(Order order);
        IEnumerable<Order> GetByCustomer(int customerId);
    }
}
