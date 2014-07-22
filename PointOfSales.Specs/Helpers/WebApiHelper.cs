using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
using PointOfSales.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PointOfSales.Specs
{
    public static class WebApiHelper
    {
        private static readonly string baseAddress = "http://localhost:9000/";

        public static List<OrderLine> GetOrderLines(int orderId)
        {
            return Get<List<OrderLine>>("api/orders/{0}/lines", orderId);
        }

        public static void AddOrderLine(int orderId, int productId, int quantity)
        {
            Post("api/orders/{0}/lines?productId={1}&quantity={2}", orderId, productId, quantity);
        }

        public static List<SalesCombination> GetSalesByProduct(int productId)
        {
            return Get<List<SalesCombination>>("api/products/{0}/sales", productId);
        }

        public static string GetJson(string url)
        {
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync(baseAddress + url).Result;
                Assert.True(HttpStatusCode.OK == response.StatusCode, response.Content.ReadAsStringAsync().Result);
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public static T Get<T>(string urlFormat, params object[] args)
        {
            return Get<T>(String.Format(urlFormat, args));
        }

        public static T Get<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(GetJson(url));
        }

        public static void Post(string urlFormat, params object[] args)
        {
            var url = String.Format(urlFormat, args);

            using(WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsync(baseAddress + url, null).Result;
                Assert.True(response.IsSuccessStatusCode, "Response status is " + response.StatusCode);
            }
        }

        public static void Post<T>(string url, T value)
        {
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsJsonAsync(baseAddress + url, value).Result;
                Assert.True(response.IsSuccessStatusCode, "Response status is " + response.StatusCode);              
            }
        }

        public static int PostAndReturnId<T>(string url, T value)
        {
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsJsonAsync(baseAddress + url, value).Result;
                Assert.True(response.IsSuccessStatusCode, "Response status is " + response.StatusCode);
                return Int32.Parse(response.Content.ReadAsStringAsync().Result);
            }
        }

        public static void Put<T>(string url, T value)
        {
            using(WebApp.Start<Startup>(url : baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.PutAsJsonAsync(baseAddress + url, value).Result;
                Assert.True(response.IsSuccessStatusCode, "Response status is " + response.StatusCode);
            }
        }
    }
}
