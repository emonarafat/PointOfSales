using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.Specs
{
    public class ProductsApi
    {
        public List<Product> GetProducts()
        {
            return WebApiHelper.Get<List<Product>>("api/products");
        }

        public List<Product> GetProducts(string search)
        {
            return WebApiHelper.Get<List<Product>>("api/products?search={0}", search);
        }
    }
}
