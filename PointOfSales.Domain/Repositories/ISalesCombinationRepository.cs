using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Domain.Repositories
{
    public interface ISalesCombinationRepository
    {
        IEnumerable<SalesCombination> GetByProductId(int productId);
    }
}
