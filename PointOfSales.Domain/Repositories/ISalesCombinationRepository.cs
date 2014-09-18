using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Domain.Repositories
{
    public interface ISalesCombinationRepository
    {
        IEnumerable<SalesCombination> GetByProductId(int productId);
        SalesCombination GetById(int id);
    }
}
