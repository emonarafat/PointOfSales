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
    public class WebApiWrapper
    {
        private static readonly string baseAddress = "http://localhost:9000/";

        public string GetJson(string url)
        {
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync(baseAddress + url).Result;
                Assert.True(HttpStatusCode.OK == response.StatusCode, response.Content.ReadAsStringAsync().Result);
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public T Get<T>(string urlFormat, params object[] args)
        {
            return Get<T>(String.Format(urlFormat, args));
        }

        public T Get<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(GetJson(url));
        }

        public void Post(string urlFormat, params object[] args)
        {
            var url = String.Format(urlFormat, args);

            using(WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsync(baseAddress + url, null).Result;
                Assert.True(response.IsSuccessStatusCode, "Response status is " + response.StatusCode);
            }
        }

        public void Post<T>(string url, T value)
        {
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsJsonAsync(baseAddress + url, value).Result;
                Assert.True(response.IsSuccessStatusCode, "Response status is " + response.StatusCode);              
            }
        }

        public int PostAndReturnId<T>(string url, T value)
        {
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsJsonAsync(baseAddress + url, value).Result;
                Assert.True(response.IsSuccessStatusCode, "Response status is " + response.StatusCode);
                return Int32.Parse(response.Content.ReadAsStringAsync().Result);
            }
        }

        public void Put<T>(string url, T value)
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
