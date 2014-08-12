using NLog;
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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
      
        [Route("")]
        public IEnumerable<Product> Get()
        {
            Logger.Info("Getting all products");
            return productRepository.GetAll();
        }        
        
        [Route("")]
        public IEnumerable<Product> Get(string search)
        {
            Logger.Info("Searching products by '{0}'", search);
            return productRepository.GetByNameOrDescription(search);
        }

        [Route("")]
        public HttpResponseMessage Post(Product product)
        {
            Logger.Info("Adding product");
            var addedProduct = productRepository.Add(product);
            return Request.CreateResponse(HttpStatusCode.Created, addedProduct);
        }
    }
}
