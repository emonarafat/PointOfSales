﻿using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
