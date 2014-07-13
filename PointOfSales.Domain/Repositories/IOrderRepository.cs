using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Domain.Repositories
{
    public interface IOrderRepository
    {
        Order GetById(int id);
        void Add(Order order);
    }
}
