using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "SearchApi",
                routeTemplate: "api/{controller}/search/{search}",
                defaults: new { action = "Search", search = RouteParameter.Optional }
            );

            appBuilder.UseNinjectMiddleware(CreateKernel)
                      .UseNinjectWebApi(config);            
        }
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            // TODO: Reuse configuration from web project
            kernel.Bind<ProductsController>().ToSelf();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<SalesController>().ToSelf();
            kernel.Bind<ISalesCombinationRepository>().To<SalesCombinationRepository>();
            return kernel;
        }
    } 
}
