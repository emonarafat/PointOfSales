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
    public class ProductsController : ApiController
    {
        private IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public IEnumerable<Product> Get()
        {
            return productRepository.GetAll();
        }

        // TODO: /api/products?search=search
        [AcceptVerbs("GET")]
        public IEnumerable<Product> Search(string search)
        {
            return productRepository.GetByNameOrDescription(search);
        }
    }
}
