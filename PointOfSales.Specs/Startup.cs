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
using System.Web.Http.Routing;
using System.Net.Http;

namespace PointOfSales.Specs
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            
            config.Routes.MapHttpRoute(
                name: "ApiRPC",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { },
                constraints: new { action = @"^[a-zA-Z]+$", httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithId",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { },
                constraints: new { id = @"^\d+$" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultGetApi",
                routeTemplate: "api/{controller}",
                defaults: new { action = "Get" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultPostApi",
                routeTemplate: "api/{controller}",
                defaults: new { action = "Post" },
                constraints: new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) }
            );

            config.Routes.MapHttpRoute(
                name: "OrderHistoryApi",
                routeTemplate: "api/customers/{customerId}/orders",
                defaults: new { controller = "orders", action = "get" },
                constraints: new { customerId = @"^\d+$" }
            );

            config.Routes.MapHttpRoute(
                name: "SearchApi",
                routeTemplate: "api/{controller}/search/{search}",
                defaults: new { action = "Search", search = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "OrderLinesApi",
                routeTemplate: "api/orderlines/order/{orderId}",
                defaults: new { controller = "OrderLines", action = "GetByOrder" }
            );

            config.Routes.MapHttpRoute(
                name: "OrderSalesApi",
                routeTemplate: "api/orders/{orderId}/sales/{salesCombinationId}",
                defaults: new { controller = "OrderLines", action = "AddSalesCombination" }
            );    

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
