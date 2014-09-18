using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PointOfSales.Domain.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByNameOrDescription(string search);
        Product GetById(int productId);
        Product Add(Product product);
    }
}
