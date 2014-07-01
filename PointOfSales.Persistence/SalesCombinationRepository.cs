using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Persistence
{
    public class SalesCombinationRepository : ISalesCombinationRepository
    {
        public IEnumerable<SalesCombination> GetByProductId(int productId)
        {
            return Enumerable.Empty<SalesCombination>();
        }
    }
}
