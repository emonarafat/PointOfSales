using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Specs
{
    public class SalesCombinationsApi : WebApiWrapper
    {
        public List<SalesCombination> GetSalesByProduct(int productId)
        {
            return Get<List<SalesCombination>>("api/products/{0}/sales", productId);
        }

        public void Post(int orderId, int salesCombinationId)
        {
            Post(String.Format("api/orders/{0}/sales/{1}", orderId, salesCombinationId), new object());
        }
    }
}
