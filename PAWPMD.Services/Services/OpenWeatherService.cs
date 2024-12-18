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
        /// <summary>
        /// Asynchronously retrieves weather data for a specific location based on latitude and longitude using the Meteoblue API.
        /// The method constructs the request URL, makes an HTTP GET request to the API, and returns the weather data as a JSON string.
        /// </summary>
        /// <param name="lat">The latitude of the location for which weather data is to be retrieved.</param>
        /// <param name="lon">The longitude of the location for which weather data is to be retrieved.</param>
        /// <param name="apiKey">The API key required to authenticate the request to the Meteoblue service.</param>
        /// <returns>A `Task` representing the asynchronous operation. The task result is a JSON string containing the weather data.</returns>
        /// <exception cref="HttpRequestException">Thrown if the HTTP request fails or the response status is not successful.</exception>

        Task<string> GetWeatherDataAsync(double lat, double lon, string apiKey);
    }

    public class OpenWeatherService : IOpenWeatherService
    {
        private static readonly HttpClient client = new HttpClient();
        /// <summary>
        /// Asynchronously retrieves weather data for a specific location based on latitude and longitude using the Meteoblue API.
        /// The method constructs the request URL, makes an HTTP GET request to the API, and returns the weather data as a JSON string.
        /// </summary>
        /// <param name="lat">The latitude of the location for which weather data is to be retrieved.</param>
        /// <param name="lon">The longitude of the location for which weather data is to be retrieved.</param>
        /// <param name="apiKey">The API key required to authenticate the request to the Meteoblue service.</param>
        /// <returns>A `Task` representing the asynchronous operation. The task result is a JSON string containing the weather data.</returns>
        /// <exception cref="HttpRequestException">Thrown if the HTTP request fails or the response status is not successful.</exception>

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
