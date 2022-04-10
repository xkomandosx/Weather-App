using JsonFlatFileDataStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
using weatherAPI.Models;

namespace weatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        string baseUrl = "https://www.metaweather.com/api/location/search/?query=";

        [HttpGet]
        public string Get()
        {
            var store = new DataStore("data.json");
            var collection = store.GetCollection<Location>().AsQueryable();

            return JsonConvert.SerializeObject(collection);
        }


        [HttpGet("{location}")]
        public string Get(string location)
        {

            var store = new DataStore("data.json");
            var collection = store.GetCollection<Location>();
            var cities = collection.AsQueryable().Where(e => e.title.ToLower() == location.ToLower());

            string result;
            if (cities.Count() > 0)
            {
                Console.WriteLine(cities);
                result = JsonConvert.SerializeObject(cities);
            }
            else
            {
                string currentUrl = baseUrl + location;
                Task<string> t = GetFromAPI(currentUrl);
           
                try
                {
                    result = t.Result;
                    JArray city = JArray.Parse(result);
                    if (city.Count() > 0)
                    {
                        foreach (var item in city)
                        {
                            var currentLocation = new Location { title = item["title"].ToString(), location_type = item["location_type"].ToString(), woeid = Int32.Parse(item["woeid"].ToString()), latt_long = item["latt_long"].ToString() };
                            collection.InsertOneAsync(currentLocation);
                        }
                        
                    }
                }
                catch
                {
                    result = "{error: Something went wrong}";
                }
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
