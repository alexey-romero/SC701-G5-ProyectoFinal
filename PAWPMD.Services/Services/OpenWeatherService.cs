using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PAWPMD.Service.Services
{
    public interface IOpenWeatherService
    {
        Task<string> GetWeatherDataAsync(double lat, double lon, string apiKey);
    }

    public class OpenWeatherService : IOpenWeatherService
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task<string> GetWeatherDataAsync(double lat, double lon ,string apiKey)
        {

            string url = $"http://my.meteoblue.com/packages/basic-1h_basic-day?lat={lat}&lon={lon}&apikey={apiKey}";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject weatherJson = JObject.Parse(responseBody);
            return weatherJson.ToString();
        }
    }
}
