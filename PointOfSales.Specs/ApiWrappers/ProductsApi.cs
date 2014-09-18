using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Specs
{
    public class ProductsApi : WebApiWrapper
    {
        public List<Product> GetProducts()
        {
            return Get<List<Product>>("api/products");
        }

        public List<Product> Get(string search)
        {
            return Get<List<Product>>("api/products?search={0}", search);
        }

        public void Post(Product product)
        {
            Post("api/products", product);
        }
    }
}
