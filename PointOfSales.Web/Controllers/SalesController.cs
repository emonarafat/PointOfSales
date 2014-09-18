using NLog;
using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PointOfSales.Web.Controllers
{
    public class SalesController : ApiController
    {
        private ISalesCombinationRepository salesCombinationRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SalesController(ISalesCombinationRepository salesCombinationRepository)
        {
            this.salesCombinationRepository = salesCombinationRepository;
        }

        [Route("api/products/{productId:int}/sales")]
        public IEnumerable<SalesCombination> GetByProduct(int productId)
        {
            Logger.Info("Getting sales combinations for product '{0}'", productId);
            return salesCombinationRepository.GetByProductId(productId);
        }
    }
}
