using PointOfSales.Domain.Model;
using PointOfSales.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PointOfSales.Web.Controllers
{
    public class SalesController : ApiController
    {
        private ISalesCombinationRepository salesCombinationRepository;

        public SalesController(ISalesCombinationRepository salesCombinationRepository)
        {
            this.salesCombinationRepository = salesCombinationRepository;
        }

        [AcceptVerbs("GET")]
        public IEnumerable<SalesCombination> Search(int search)
        {
            // TODO: Rename parameter
            return salesCombinationRepository.GetByProductId(search);
        }
    }
}
