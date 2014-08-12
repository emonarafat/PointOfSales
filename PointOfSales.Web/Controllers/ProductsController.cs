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
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
      
        [Route("")]
        public IEnumerable<Product> Get()
        {
            return productRepository.GetAll();
        }        
        
        [Route("")]
        public IEnumerable<Product> Get(string search)
        {
            return productRepository.GetByNameOrDescription(search);
        }

        [Route("")]
        public Product Post(Product product)
        {
            return productRepository.Add(product);
        }
    }
}
