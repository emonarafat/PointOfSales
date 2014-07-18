using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace PointOfSales.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
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
        }
    }
}
