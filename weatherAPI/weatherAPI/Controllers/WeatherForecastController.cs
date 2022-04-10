using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace weatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        string baseUrl = "https://www.metaweather.com/api/location/";

        [HttpGet("{woeid}")]
        public string Get(string woeid)
        {


            string currentUrl = baseUrl + woeid;
            Task<string> t = GetFromAPI(currentUrl);

            string result;
            try
            {
                result = t.Result;
            }
            catch
            {
                result = "{error: Something went wrong}";
            }
            return result;
        }

        static async Task<string> GetFromAPI(string url)
        {
            HttpClientHandler handler = new HttpClientHandler();

            // ... Use HttpClient.            
            HttpClient client = new HttpClient(handler);

           // var byteArray = Encoding.ASCII.GetBytes("");
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            HttpResponseMessage response = await client.GetAsync(url);
            HttpContent content = response.Content;

            // ... Check Status Code                                
            //Console.WriteLine("Response StatusCode: " + (int)response.StatusCode);

            // ... Read the string.
            return await content.ReadAsStringAsync();

        }
    }
}
