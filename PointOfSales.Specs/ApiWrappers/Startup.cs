using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

using System.Reflection;
using PointOfSales.Web.Controllers;
using PointOfSales.Domain.Repositories;
using PointOfSales.Persistence;

namespace PointOfSales.Specs
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            appBuilder.UseNinjectMiddleware(CreateKernel)
                      .UseNinjectWebApi(config);
        }
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            // TODO: Reuse configuration from web project
            kernel.Bind<ProductsController>().ToSelf();
            kernel.Bind<SalesController>().ToSelf();
            kernel.Bind<OrdersController>().ToSelf();
            kernel.Bind<OrderLinesController>().ToSelf();

            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<ISalesCombinationRepository>().To<SalesCombinationRepository>();
            kernel.Bind<IOrderLineRepository>().To<OrderLineRepository>();
            kernel.Bind<IOrderRepository>().To<OrderRepository>();
            kernel.Bind<ICustomerRepository>().To<CustomerRepository>();
            return kernel;
        }
    } 
}
