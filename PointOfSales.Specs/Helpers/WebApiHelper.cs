using Microsoft.Owin.Hosting;
using Newtonsoft.Json;
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

        public static T Get<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(GetJson(url));
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
    }
}
